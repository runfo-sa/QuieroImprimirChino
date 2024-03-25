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
// Menu Proyecto->Agregar referencia-> seccion "framework", tildar System.Configuration
using System.Configuration;

namespace QuieroImprimirChino
{
    public partial class frmEtiquetasIsrael : Form
    {
        AccesoSQL datos;
        String vencimiento;
        String congelamiento;
        String comercializacion;
        LeerArchivoTexto archivo;
        Configuration configuracion; // objeto para manejar la config por XML
        private String directorioEtiquetas; // aqui va la ubicacion de los archivos ZPL

        private int cantidadCopias;
        public frmEtiquetasIsrael()
        {
            InitializeComponent();
            inicializarCombo();

            inicializarComboCategorias();

            archivo = new LeerArchivoTexto();
            cargarConfiguracion();
            dateTimePickerFaena.Value = calculaFechaFaenaSugerida(dateTimePicker1.Value);
        }

        //https://medium.com/jinweijie/how-to-print-chinese-characters-in-zpl-4b4bc79bdd8f
            //https://zega-labels.de/mediafiles/Sonstiges/Zebra_Flash_Font_-_Simplified_Chinese.pdf
           
        private void inicializarCombo()
        {
            datos = new AccesoSQL();
            datos.conectar();
            datos.leerMercaderias(ref comboMercaderias, "", "");
            
        }
        private void calcularVencimiento()
        {
            // contando con una mercaderia seleccionada y una fecha, obtiene la fecha de vencimiento
            String fechaReferencia;
            String fechaReferenciaFaena;
            String mercaderia;
            dateTimePicker1.CustomFormat = "yyyyMMdd";
            dateTimePickerFaena.CustomFormat = "yyyyMMdd";
            // mercaderia = comboMercaderias.SelectedText;
            //fechaReferencia.Day = dateTimePicker1.Value.Day;
            mercaderia = comboMercaderias.SelectedValue.ToString();
            //fechaReferencia = (String)dateTimePicker1.Value.Year.ToString();
            //fechaReferencia += (String)dateTimePicker1.Value.Month.ToString();
            //fechaReferencia += (String)dateTimePicker1.Value.Day.ToString();
            fechaReferencia = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd");
            fechaReferenciaFaena = Convert.ToDateTime(dateTimePickerFaena.Value).ToString("yyyyMMdd");
            //Console.WriteLine(fechaReferencia);
            // MessageBox.Show(datos.calcularVencimiento(mercaderia, fechaReferencia));
            vencimiento = datos.calcularVencimiento(mercaderia, fechaReferencia, fechaReferenciaFaena);
        }
        private void calcularComercializacion()
        {
            // contando con una mercaderia seleccionada y una fecha, obtiene la fecha de comercializacion
            String fechaReferencia;
            String fechaReferenciaFaena;
            String mercaderia;
            dateTimePicker1.CustomFormat = "yyyyMMdd";
            dateTimePickerFaena.CustomFormat = "yyyyMMdd";

            mercaderia = comboMercaderias.SelectedValue.ToString();

            fechaReferencia = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd");
            fechaReferenciaFaena = Convert.ToDateTime(dateTimePickerFaena.Value).ToString("yyyyMMdd");

            comercializacion = datos.calcularComercializacion(mercaderia, fechaReferencia, fechaReferenciaFaena);
        }
        private void calcularCongelamiento()
        {
            // contando con una mercaderia seleccionada y una fecha, obtiene la fecha de congelado
            String fechaReferencia;
            String mercaderia;
            dateTimePicker1.CustomFormat = "yyyyMMdd";
            mercaderia = comboMercaderias.SelectedValue.ToString();
            fechaReferencia = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyyMMdd");
            congelamiento = datos.calcularCongelamiento(mercaderia, fechaReferencia);
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
                    MessageBox.Show("Error al intentar leer la configuración de cantidad de copias predeterminadas de etiqueta.", "Changos");
                    // si no puedo levantar ningun valor, entonces lo fijo en 1 y lo guardo
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
                MessageBox.Show("Excepcion al intentar leer la configuración: " + exc.Data, "Changos");
            }
            try
            {
                //comboImpresoras.DataSource = ConfigurationManager.AppSettings["Impresora"].Split(',');
                Console.WriteLine("bogus");
            }
            catch (ConfigurationErrorsException)
            {

                MessageBox.Show("No se pudo leer la configuracion de impresoras", "Cáspita");
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
                MessageBox.Show("No se pudo leer la ubicación de las etiquetas ZPL (error de acceso al archivo)", "Cáspita");
            }
            catch (NullReferenceException)
            {
                // si no pudo leer ninguna impresora guarda la predeterminada
                MessageBox.Show("No se pudo leer la ubicación de las etiquetas ZPL (puede ser que falte en el archivo XML?)", "Cáspita");
            }
            numericCopias.Value = cantidadCopias;

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
                MessageBox.Show("No se pudo leer la impresora predeterminada del archivo de configuracion. " + exc.Message, "Caracoles!");
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
                MessageBox.Show("No se pudieron obtener las impresoras del sistema. El servicio SPOOL está activo? " + exc.Message);
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
                    MessageBox.Show("No se pudo determinar la impresora predeterminada. " + exc.ToString() + " - " + exc.GetType().Name, "Diantres!");
                }
            }
            return(impresoraDefault);
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            String cadenaImpresion;
            if (comboMercaderias.SelectedItem == null)
                MessageBox.Show("Debe seleccionar un producto de la lista");
            else
            {
                calcularVencimiento();
                calcularCongelamiento();
                calcularComercializacion();
                if (cargarArchivo())
                {
                    cadenaImpresion = datos.obtenerCampos(comboMercaderias.SelectedValue.ToString(), archivo.textoZPL);
                    cadenaImpresion = reemplazarFechas(cadenaImpresion);
                    for (int i = 1; i <= numericCopias.Value; i++)
                        ImpresionChino.RawPrinterHelper.SendStringToPrinter(comboImpresoras.SelectedValue.ToString(), cadenaImpresion);
                }
                else
                    MessageBox.Show("Error al intentar leer la etiqueta \r\n" + archivo.leerError());

            }
        }
        private String reemplazarFechas(String archivoZPL)
        {
            String retorno;
            String fechaProd;
            String fechaFaena;
            // 7-12-2021 Agrego soporte para fecha de faena
            fechaProd = Convert.ToDateTime(dateTimePicker1.Value).ToString("dd/MM/yyyy");
            fechaFaena = Convert.ToDateTime(dateTimePickerFaena.Value).ToString("dd/MM/yyyy");
            retorno = archivoZPL.Replace("[@identificadores.dFechaProduccion;FFdd/MM/yyyy@]",fechaProd);
            retorno = retorno.Replace("[@PrimariaDatosFrigo.dFechaFaena;FFdd/MM/yyyy@]", fechaFaena);
            retorno = retorno.Replace("[@FechaVencimiento.dFecha;FFdd/MM/yyyy@]",vencimiento);
            retorno = retorno.Replace("[@FechaCongelado.dFecha;FFdd/MM/yyyy@]", congelamiento);
            retorno = retorno.Replace("[@FechaComercializacion.dFecha;FFdd/MM/yyyy@]", comercializacion);            
            return retorno;
        }
        private bool cargarArchivo() {
            //nombreArchivo = @"Z:\Sistemas\Etiquetas\Documentación y apps\Israel\PRIMARIA_ISRAEL (caja)";
            //nombreArchivo += ".e01";
            bool retorno;
            String mercaderia;
            String archivoOError;
            retorno = true;
            // mercaderia = comboMercaderias.SelectedText;
            //fechaReferencia.Day = dateTimePicker1.Value.Day;
            mercaderia = comboMercaderias.SelectedValue.ToString();
            archivoOError = datos.buscarArchivoFormato(mercaderia);
            if (archivoOError.StartsWith("ERROR"))
            {
                MessageBox.Show(archivoOError);
                retorno = false;
            }
            else
            {
                //archivo.cargar(@"D:\PiQuatro\Etiquetas\" + archivoOError + ".e01");
                if ((directorioEtiquetas != "") && (archivo.leerError() == ""))
                {
                    if (!(directorioEtiquetas.EndsWith("\\"))) // asi no me importa si la ruta termina en contrabarra o no
                        directorioEtiquetas  += "\\";
                    if (!(archivo.cargar(directorioEtiquetas + archivoOError + ".e01")))
                        retorno = false;
                }
                else
                {
                    if (directorioEtiquetas == "")
                        MessageBox.Show("No se pudo determinar la ubicacion de los formatos de etiqueta", "Recorcholis");
                    else
                        MessageBox.Show(archivo.leerError());
                    retorno = false;
                }
            }
            return retorno;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // vamos a ayudarle al usuario con la fecha de faena
            dateTimePickerFaena.Value = calculaFechaFaenaSugerida(dateTimePicker1.Value);
        }
        private DateTime calculaFechaFaenaSugerida(DateTime produccion)
        {
            // 7-12-21 JEH
            // por default la fecha de faena es dos dias atras
            // pero si eso cae sabado o domingo, lo llevo al viernes
            DateTime faena;
            faena = produccion.AddDays(-2);
            if (faena.DayOfWeek == DayOfWeek.Sunday)
                faena = faena.AddDays(-2);
            if (faena.DayOfWeek == DayOfWeek.Saturday)
                faena = faena.AddDays(-1);
            return faena;
        }

        //16/06/2023 AAL
        // Simón solicitó una manera de filtrar los productos que se muestran... pero no hay forma de que eso se identifique por Pi4, así que lo identificamos del nombre.
        private void ActualizarMercaderiaBoton_Click(object sender, EventArgs e)
        {
            string filtro = "";
            string halak = "";
            string categoria = CategoriaCombo.SelectedValue.ToString();

            if (!categoria.Equals("TODOS"))
            {
                filtro = categoria.IndexOf("cong", 0, StringComparison.InvariantCultureIgnoreCase) != -1 ? "cong" : "enfr";
                halak = categoria.IndexOf("halak", 0, StringComparison.InvariantCultureIgnoreCase) != -1 ? "halak" : "kosher";
            }

            datos.leerMercaderias(ref comboMercaderias, filtro, halak);
        }

        //16/06/2023 AAL
        // Las "categorias" se obtienen de una tabla en la db AuxiliarEtiquetas.
        private void inicializarComboCategorias()
        {
            datos.leerCategorias(ref CategoriaCombo);
        }


    }
}
