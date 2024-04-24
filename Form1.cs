using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Impresion;
using System.Drawing.Printing;
using ImpresionChino;
using System.Globalization;
using System.Deployment.Application;
using System.Runtime.InteropServices;

// Menu Proyecto->Agregar referencia-> seccion "framework", tildar System.Configuration
using System.Configuration;

namespace QuieroImprimirChino
{
    public partial class frmEtiquetasIsrael : Form
    {
        AccesoSQL datos;
        String vencimiento;
        LeerArchivoTexto archivo;
        Configuration configuracion; // objeto para manejar la config por XML
        private String directorioEtiquetas; // aqui va la ubicacion de los archivos ZPL

        private int cantidadCopias;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int Wmsg, int wpram, int lparam);

        public frmEtiquetasIsrael()
        {
            InitializeComponent();
            if (ApplicationDeployment.IsNetworkDeployed)
                Lbl_Version.Text = "Versión: " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            inicializarCombo();
            archivo = new LeerArchivoTexto();
            cargarConfiguracion();
        }
      

        //https://medium.com/jinweijie/how-to-print-chinese-characters-in-zpl-4b4bc79bdd8f
        //https://zega-labels.de/mediafiles/Sonstiges/Zebra_Flash_Font_-_Simplified_Chinese.pdf
        //https://stackoverflow.com/questions/2044676/net-code-to-send-zpl-to-zebra-printers

        private void inicializarCombo()
        {
            datos = new AccesoSQL();
            datos.conectar();
            datos.leerMercaderias(ref comboMercaderias);
            
        }
        private void calcularVencimiento()
        {
            // contando con una mercaderia seleccionada y una fecha, obtiene la fecha de vencimiento
            String fechaReferencia;
            String mercaderia;
            dateTimePicker1.CustomFormat = "yyyyMMdd";
            // mercaderia = comboMercaderias.SelectedText;
            //fechaReferencia.Day = dateTimePicker1.Value.Day;
            mercaderia = comboMercaderias.SelectedValue.ToString();
            //fechaReferencia = (String)dateTimePicker1.Value.Year.ToString();
            //fechaReferencia += (String)dateTimePicker1.Value.Month.ToString();
            //fechaReferencia += (String)dateTimePicker1.Value.Day.ToString();
            fechaReferencia = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd");
            //Console.WriteLine(fechaReferencia);
            // MessageBox.Show(datos.calcularVencimiento(mercaderia, fechaReferencia));
            vencimiento = datos.calcularVencimiento(mercaderia, fechaReferencia);
        }
        private void cargarConfiguracion()
        {
            configuracion = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                data.Add(new KeyValuePair<string, string>(printer, printer));
            }
            comboImpresoras.DataSource = null;
            comboImpresoras.Items.Clear();
            comboImpresoras.DataSource = new BindingSource(data, null);
            comboImpresoras.DisplayMember = "Value";
            comboImpresoras.ValueMember = "Key";
            // asi es como selecciono la impresora default en el combo
            // si la default del XML existe en el sistema, se selecciona esa, sino la default de windows
            comboImpresoras.SelectedText = cualEsLaImpresoraDefault();
            comboImpresoras.SelectedValue = cualEsLaImpresoraDefault();
            try
            {

                if (!(int.TryParse(configuracion.AppSettings.Settings["CantidadCopias"].Value, out cantidadCopias)))
                {
                    MessageBox.Show("Error al intentar leer la configuración de cantidad de copias predeterminadas de etiqueta.", "Changos", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    // si no puedo levantar ningun valor, entonces lo fijo en 1 y lo guardo
                    cantidadCopias = 1;
                    configuracion.AppSettings.Settings.Add("CantidadCopias", "1");
                    configuracion.Save();
                }
            }
            catch (NullReferenceException) {
                // si tira este error es porque no existe la configuracion
                configuracion.AppSettings.Settings.Add("CantidadCopias", "1");
                configuracion.Save();
            }
            catch (Exception exc) {
                MessageBox.Show("Excepcion al intentar leer la configuración: " + exc.Data, "Changos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                //comboImpresoras.DataSource = ConfigurationManager.AppSettings["Impresora"].Split(',');
                Console.WriteLine("bogus");
            }
            catch (ConfigurationErrorsException)
            {

                MessageBox.Show("No se pudo leer la configuracion de impresoras", "Cáspita", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException)
            {
                // si no pudo leer ninguna impresora guarda la predeterminada
                configuracion.AppSettings.Settings.Add("Impresora", data[0].Value);
                configuracion.Save();
            }
            try
            {
                directorioEtiquetas = ConfigurationManager.AppSettings["DirectorioEtiquetas"].ToString();
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("No se pudo leer la ubicación de las etiquetas ZPL (error de acceso al archivo)", "Cáspita", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException)
            {
                // si no pudo leer ninguna impresora guarda la predeterminada
                MessageBox.Show("No se pudo leer la ubicación de las etiquetas ZPL (puede ser que falte en el archivo XML?)", "Cáspita", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if ((cantidadCopias < 51) && (cantidadCopias > 0))
                numericCopias.Value = cantidadCopias;
            else
            {
                numericCopias.Value = 1;
                MessageBox.Show("La cantidad de copias predeterminada está fuera del rango admitido (entre 1 y 50)", "Cáspita", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private String cualEsLaImpresoraDefault()
        {
            String impresoraDefault = ""; // como grave devuelve blanco
            // existe la impresora del XML de configuracion en el sistema?
            // si es asi, entonces la devuelvo como default
            // sino devuelvo la impresora defaul del sistema
            bool existe = false;
            try
            {
                impresoraDefault = ConfigurationManager.AppSettings["Impresora"].ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show("No se pudo leer la impresora predeterminada del archivo de configuracion. " + exc.Message, "Caracoles!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {   
                // me fijo si la impresora esta entre las del sistema
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    if (printer == impresoraDefault)
                    {
                        existe = true;
                        break;  // para dejar de dar vueltas en el foreach
                    }
                }
            }
            catch (Exception exc) { 
            // no se pudieron enumerar
                MessageBox.Show("No se pudieron obtener las impresoras del sistema. El servicio SPOOL está activo? " + exc.Message, "Changos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!existe)
            {
                try
                {
                    PrinterSettings settings = new PrinterSettings();
                    impresoraDefault = settings.PrinterName;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se pudo determinar la impresora predeterminada. " + exc.ToString() + " - " + exc.GetType().Name, "Diantres!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return(impresoraDefault);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            /*
             * Valida el año de produccion (entre 2010 y 2050)
             * Invoca el metodo para calcular el vencimiento
             * Completa los campos con un metodo
             * Envia a imprimir (con otra clase)
             
             */
            String cadenaImpresion;
            if (comboMercaderias.SelectedItem == null)
                MessageBox.Show("Debe seleccionar un producto de la lista", "Miralo eh...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if ((dateTimePicker1.Value.Year < 2050) && (dateTimePicker1.Value.Year > 2010))
                {
                    calcularVencimiento();
                    if (cargarArchivo())
                    {
                        cadenaImpresion = datos.obtenerCampos(comboMercaderias.SelectedValue.ToString(), archivo.textoZPL);
                        cadenaImpresion = reemplazarFechas(cadenaImpresion);
                        for (int i = 1; i <= numericCopias.Value; i++)   ////saco esta linea para mandar la cantidad como variable al diseño de la etiqueta y que sea mas rapida la impresion. hace el envio una sola vez
                            ImpresionChino.RawPrinterHelper.SendStringToPrinter(comboImpresoras.SelectedValue.ToString(), cadenaImpresion);
                    }
                }
                else
                    MessageBox.Show("La fecha indicada está fuera del rango admitido", "Ubicate en tiempo y espacio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private String reemplazarFechas(String archivoZPL)
        {
            String retorno;
            String fechaProd;
            String Lote;
            String fechaFaena;
            String mercaderia;
            //int domi = 0;
            //int sab = 6;
            //int diasab =6;
            //int dia;
            // generamos la fecha de produccion en base al datepicker
            fechaProd = Convert.ToDateTime(dateTimePicker1.Value).ToString("dd/MM/yyyy");
            Lote = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd");
            // generamos la fecha de faena restando uno a la fecha de produccion
            mercaderia = comboMercaderias.SelectedValue.ToString();
            if ( mercaderia == "247" || mercaderia =="238" || mercaderia == "646")
            {
                String fechaReferencia;

                dateTimePicker1.CustomFormat = "yyyyMMdd";

                mercaderia = comboMercaderias.SelectedValue.ToString();

                fechaReferencia = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd");
                fechaFaena =datos.calcularFaena(fechaReferencia);
                /**fechaFaena = Convert.ToDateTime(dateTimePicker1.Value.AddDays(-2)).ToString("dd/MM/yyyy");
                DateTime domingo = Convert.ToDateTime(fechaFaena);
                DateTime sabado = Convert.ToDateTime(fechaFaena);
                //fechaFaena = Convert.ToDateTime(dateTimePicker1.Value.AddDays(-2)).ToString("dd/MM/yyyy");
                byte dia7 = (byte)domingo.DayOfWeek;
                byte dia6 = (byte)sabado.DayOfWeek;
                //byte dia6 = (byte)domingo.DayOfWeek;
                dia = dia7;//.ToString("dddd", new CultureInfo("es-ES"));
                //diasab = dia6;
               if (domi == dia)
               {
                   fechaFaena = Convert.ToDateTime(dateTimePicker1.Value.AddDays(-4)).ToString("dd/MM/yyyy"); ;
               }
               else if (diasab == dia6)
                    {
                    fechaFaena = Convert.ToDateTime(dateTimePicker1.Value.AddDays(-3)).ToString("dd/MM/yyyy"); ;
                }
                else
                {
                    fechaFaena = Convert.ToDateTime(dateTimePicker1.Value.AddDays(-6)).ToString("dd/MM/yyyy");
                }**/
            }
            
            else 
            {
                fechaFaena = Convert.ToDateTime(dateTimePicker1.Value.AddDays(-1)).ToString("dd/MM/yyyy");
            }
            
            retorno = archivoZPL.Replace("[@identificadores.dFechaFaena;FFdd/MM/yyyy@]", fechaFaena);
            retorno = retorno.Replace("[@identificadores.dFechaProduccion;FFdd/MM/yyyy@]", fechaProd);
            retorno = retorno.Replace("[@FechaVencimiento.dFecha;FFdd/MM/yyyy@]",vencimiento);
            retorno = retorno.Replace("[@identificadores.dFechaProduccion@]", Lote);
            return retorno;
        }
        private bool cargarArchivo() {
            //nombreArchivo = @"Z:\Sistemas\Etiquetas\Documentación y apps\Israel\PRIMARIA_ISRAEL (caja)";
            //nombreArchivo += ".e01";
            bool retorno;
            String mercaderia;
            String archivoOError;
            String resultadoCargaArchivo;
            retorno = true;
            // mercaderia = comboMercaderias.SelectedText;
            //fechaReferencia.Day = dateTimePicker1.Value.Day;
            mercaderia = comboMercaderias.SelectedValue.ToString();
            archivoOError = datos.buscarArchivoFormato(mercaderia);
            if (archivoOError.StartsWith("ERROR"))
            {
                MessageBox.Show(archivoOError, "APA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                retorno = false;
            }
            else
            {
                //archivo.cargar(@"D:\PiQuatro\Etiquetas\" + archivoOError + ".e01");
                if ((directorioEtiquetas != "") && (archivo.leerError() == ""))
                {
                    if (!(directorioEtiquetas.EndsWith("\\"))) // asi no me importa si la ruta termina en contrabarra o no
                        directorioEtiquetas  += "\\";
                    resultadoCargaArchivo = archivo.cargar(directorioEtiquetas + archivoOError + ".e01");
                    if (resultadoCargaArchivo != "")
                    {
                        MessageBox.Show("Hubo un error al tratar de leer el archivo: " + resultadoCargaArchivo, "UPA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        retorno = false;
                    }

                }
                else
                {
                    if (directorioEtiquetas == "")
                        MessageBox.Show("No se pudo determinar la ubicacion de los formatos de etiqueta", "Recorcholis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(archivo.leerError(), "Me lleva...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    retorno = false;
                }
            }
            return retorno;
        }

        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Generador de etiquetas para Israel desarrollado por el Departamento Sistemas de Runfo", "Sistemas@Runfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Printer IP Address and communication port
            string ipAddress = "192.168.22.99";
            int port = 9100;

            // ZPL Command(s)
            string ZPLString =
             "^XA" +
             "^SEE:ANMDS.TTF^CI28^FH_" +
             "^FO50,50" +
             "^A0N50,50" +
             "^FDHello, World!^FS" +

             "^FO50,50^A1,145,145^FD e盒呆艾丙^FS" +
             "^FO50,200^A1,132,132^FD 盒呆艾丙^FS^XZ";
             

            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(ipAddress, port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer =
                new System.IO.StreamWriter(client.GetStream());
                writer.Write(ZPLString);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                // Catch Exception
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String cadenaImpresion;
            calcularVencimiento();
            cargarArchivo();
            //String mercaderia;
            //mercaderia = comboMercaderias.SelectedValue.ToString();
            //archivo.cargar(directorioEtiquetas + "\\" + datos.buscarArchivoFormato(mercaderia) + ".e01");
            archivo.cargar("pruebaChina.e01");

            cadenaImpresion = datos.obtenerCampos(comboMercaderias.SelectedValue.ToString(), "pruebaChina.e01");
            cadenaImpresion = reemplazarFechas(cadenaImpresion);
            ImpresionChino.RawPrinterHelper.SendTextFileToPrinter(comboImpresoras.SelectedValue.ToString(), cadenaImpresion);
            
        }

        private void comboMercaderias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmEtiquetasIsrael_Load(object sender, EventArgs e)
        {
            
        }
    }
}
