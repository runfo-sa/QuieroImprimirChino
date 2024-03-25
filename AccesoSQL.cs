using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Specialized;

namespace QuieroImprimirChino
{
    class AccesoSQL
    {
        private SqlConnection cnn;
        
        //private SqlDataReader lector;
        public bool conectar() {
            // conecta a la DB y devuelve true si fue exitoso o false si no lo fue
            string connetionString;
            bool retorno;
            retorno = true;

            // 25/03/2024, AAL.
            // Vamos a subir este código fuente a Git, y como tal necesitamos revisarlo antes para que no haya una fuga de credenciales.
            // Moví la cadena de conexión a otro archivo, conexion.config y agregué que la lea de ahí.
            try
            {
                connetionString = ((NameValueCollection)ConfigurationManager.GetSection("conexion"))["cadConexion"];
                cnn = new SqlConnection(connetionString);
                cnn.Open();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                retorno = false;
            }

          return (retorno);
        }
        //16/06/2023 - AAL
        public bool leerCategorias(ref System.Windows.Forms.ComboBox combo)
        {
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            if (cnn.State == ConnectionState.Open)
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlCommand comando = new SqlCommand("dbo.listaCategorias", cnn);
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(comando);
                    adap.Fill(dt);

                    combo.DataSource = dt;
                    combo.DisplayMember = "Valor";
                    combo.ValueMember = "Valor";
                }
                catch (SqlException exc)
                {
                    data.Add(new KeyValuePair<string, string>("Error", "Error de SQL al leer la mercaderia: " + exc.Message));
                    combo.DataSource = null;
                    combo.Items.Clear();
                    combo.DataSource = new System.Windows.Forms.BindingSource(data, null);
                    combo.DisplayMember = "Value";
                    combo.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    data.Add(new KeyValuePair<string, string>("Error", "Error al leer la mercaderia: " + ex.Message));
                    combo.DataSource = null;
                    combo.Items.Clear();
                    combo.DataSource = new System.Windows.Forms.BindingSource(data, null);
                    combo.DisplayMember = "Value";
                    combo.ValueMember = "Key";
                }
            }
            return true;
        }
        public bool leerMercaderias(ref System.Windows.Forms.ComboBox combo, String filtro, String halak)
        {
            // recibe una referencia a un combobox para cargarle el DataSource con la mercaderia
            // el SP listaMercaderia es el que filtra los productos a incluir
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            if (cnn.State == ConnectionState.Open)
            {
                //String consultaMercaderias;
                try
                {
                    DataTable dt = new DataTable();
                    SqlCommand comando = new SqlCommand("dbo.listaMercaderia", cnn);
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(comando);
                    adap.Fill(dt);
                    //16/06/2023 - AAL
                    // Para procesar las mercaderías y mostrar solo la que está seleccionada... otro enfoque que se me ocurrio es agregar el filtro del lado de la base, pero le deja el calculo al servidor.
                    foreach(DataRow row in dt.Rows)
                    {
                        if (!(row["Descripcion"].ToString().IndexOf(filtro, 0, StringComparison.InvariantCultureIgnoreCase) != -1 &&
                            row["Descripcion"].ToString().IndexOf(halak, 0, StringComparison.InvariantCultureIgnoreCase) != -1))
                        { 
                            row.Delete();
                        }
                    }
                    dt.AcceptChanges();
                     
                    combo.DataSource = dt;
                    combo.DisplayMember = "Descripcion";
                    combo.ValueMember = "id";                  
                                        
                }
                catch (SqlException exc) {
                    data.Add(new KeyValuePair<string, string>("Error", "Error de SQL al leer la mercaderia: " + exc.Message));
                    combo.DataSource = null;
                    combo.Items.Clear();
                    combo.DataSource = new System.Windows.Forms.BindingSource(data, null);
                    combo.DisplayMember = "Value";
                    combo.ValueMember = "Key";
                }
                catch (Exception ex) {
                    data.Add(new KeyValuePair<string, string>("Error", "Error al leer la mercaderia: " + ex.Message));
                    combo.DataSource = null;
                    combo.Items.Clear();
                    combo.DataSource = new System.Windows.Forms.BindingSource(data, null);
                    combo.DisplayMember = "Value";
                    combo.ValueMember = "Key";
                }
            }
            return true;
        }
        public void desconectar()
        {
            cnn.Close();
        }


        public String calcularCongelamiento(String idMercaderia, String fechaReferencia)
        {
            // recibe un id de mercaderia y una fecha de produccion 
            // en base a eso consulta un SP de SQL "calcularCongelamiento"
            // que calcula el vencimiento segun lo que tiene parametrizado Twins para esa mercaderia
            // devuelve la fecha de vencimiento como cadena de texto
            // si no la pudo determinar, devuelve "ERROR"
            String fecha;
            fecha = "ERROR";
            SqlDataReader rdr;
            SqlCommand comando = new SqlCommand("dbo.calcularCongelamiento", cnn);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter ("@id", idMercaderia));
            comando.Parameters.Add(new SqlParameter ("@fechaReferencia", fechaReferencia));
            try
            {
                rdr = comando.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fecha = rdr["Congelamiento"].ToString();
                    if (fecha == null) // nunca debiera volver nulo, pero con esto evito que mate toda la cadena
                        fecha = "";
                } // si no leyo nada queda con la cadena "ERROR" del comienzo
                rdr.Close();
            }
            catch (SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return fecha;
        }
        public String calcularVencimiento(String idMercaderia, String fechaReferencia, String fechaReferenciaFaena)
        {
            // recibe un id de mercaderia y una fecha de produccion 
            // en base a eso consulta un SP de SQL "calcularVencimiento"
            // que calcula el vencimiento segun lo que tiene parametrizado Twins para esa mercaderia
            // devuelve la fecha de vencimiento como cadena de texto
            // si no la pudo determinar, devuelve "ERROR"
            String fecha;
            fecha = "ERROR";
            SqlDataReader rdr;
            SqlCommand comando = new SqlCommand("dbo.calcularVencimiento", cnn);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter ("@id", idMercaderia));
            comando.Parameters.Add(new SqlParameter ("@fechaReferencia", fechaReferencia));
            comando.Parameters.Add(new SqlParameter("@fechaReferenciaFaena", fechaReferenciaFaena));
            try
            {
                rdr = comando.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    //Console.WriteLine(rdr["Vencimiento"]);
                    fecha = rdr["Vencimiento"].ToString();
                } // si no leyo nada queda con la cadena "ERROR" del comienzo
                rdr.Close(); //Si la lectura falla y no cierra el rdr falla... A.Luquez - 15/06/2023
            }
            catch (SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return fecha;
        }

        public String calcularComercializacion(String idMercaderia, String fechaReferencia, String fechaReferenciaFaena)
        {
            // recibe un id de mercaderia y una fecha de produccion 
            // en base a eso consulta un SP de SQL "calcularFechaComercializacion"
            // que calcula la fecha de comercializacion segun lo que tiene parametrizado Twins para esa mercaderia
            // devuelve la misma como cadena de texto
            // si no la pudo determinar, devuelve "ERROR"
            String fecha;
            fecha = "ERROR";
            SqlDataReader rdr;
            SqlCommand comando = new SqlCommand("dbo.calcularFechaComercializacion", cnn);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@id", idMercaderia));
            comando.Parameters.Add(new SqlParameter("@fechaReferencia", fechaReferencia));
            comando.Parameters.Add(new SqlParameter("@fechaReferenciaFaena", fechaReferenciaFaena));
            try
            {
                rdr = comando.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fecha = rdr["FechaComercializacion"].ToString();
                } // si no leyo nada queda con la cadena "ERROR" del comienzo
                rdr.Close();
            }
            catch (SqlException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return fecha;
        }
        public String buscarArchivoFormato(String idMercaderia)
        {
            // consulto en SQL cual es el formato de etiqueta asignado en Twins para la mercaderia
            // devuelve el nombre de archivo 
            // si se produjo un error devuelve la cadena ERROR seguida de una descripcion
            String archivo;
            archivo = "ERROR";
            SqlDataReader rdr;
            SqlCommand comando = new SqlCommand("dbo.buscarArchivoFormato", cnn);
            try
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@id", idMercaderia));
                rdr = comando.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    //Console.WriteLine(rdr["Vencimiento"]);
                    archivo = rdr["nombreArchivo"].ToString();
                } // si no leyo nada queda con la cadena "ERROR" del comienzo
                rdr.Close();
            }
            catch (SqlException exc)
            {
                archivo += " de SQL " + exc.Message;
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                archivo += " general " + exc.Message;
                Console.WriteLine(exc.Message);
            }
            return archivo;
        }
        public String obtenerCampos(String idMercaderia, String archivoZPL)
        {
            // a partir de un id de mercaderia y un String ZPL, ejecuta un SP para leer los campos 
            // y hace busqueda y reemplazo para cargar cada valor
            // los valores los trae con un SP llamado obtenerCampos, el que recibe el id de mercaderia por parametro
            // las unicas variables que no se cargan aqui en el ZPL son la fecha de produccion y vencimiento 
            // lamentablemente si hay nuevos campos hay que tocar este codigo, no me gusta eso
            // hay un ZPL de error embebido en el codigo, cuando ocurra un error se imprime 
            // este y se carga el mensaje de error como parte del formato
            String trad1 = "";
            String trad2 = ""; // no lo uso, pero lo dejo 07-12-2021
            String trad5 = "";
            String trad3 = "";
            String trad6 = "";
            String trad7 = "";
            String trad8 = "";
            String trad9 = "";
            String trad17 = "";
            String definicionCuarto = "";
            String ean = "";
            String descripcion = "";
            String descSenasa = "";
            String codigo = "";
            String codSenasa = "";
            String nuevoArchivoZPL = "";
            SqlDataReader rdr;
            String archivoZPLError;
            archivoZPLError = @"^XA
                ^FX Etiqueta ERROR
                ^FX Version 1.0
                ^FX Fecha ultima modificacion: 05/01/2020
                ^LH10,25
                ^FT0,500^A0N,150^FB1121,1,0,C^FDERROR [ERROR]^FS
                ^PQ1,0,1,Y
                ^XZ";
            SqlCommand comando = new SqlCommand("dbo.obtenerCampos", cnn);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add(new SqlParameter("@id", idMercaderia));
            try
            {
                rdr = comando.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    trad1 = rdr["Trad1"].ToString();
                    trad5 = rdr["Trad5"].ToString();
                    trad2 = rdr["Trad2"].ToString();
                    trad3 = rdr["Trad3"].ToString();
                    trad6 = rdr["Trad6"].ToString();
                    trad7 = rdr["Trad7"].ToString();
                    trad8 = rdr["Trad8"].ToString();
                    trad9 = rdr["Trad9"].ToString(); // 13/06/2023 - Acá tiraba exception porque el SP no tenía el la columna Trad9, la agregué y nos volvimos todos amigos; Alcides Luquez.
                    trad17 = rdr["Trad17"].ToString();
                    definicionCuarto = rdr["DefinicionCuarto"].ToString();
                    ean = rdr["sEan"].ToString();
                    descripcion = rdr["sDescripcion"].ToString();
                    descSenasa = rdr["sDescSenasa"].ToString();
                    codigo = rdr["scodigo"].ToString();
                    codSenasa = rdr["sCodSenasa"].ToString();

                } // si no leyo nada queda con la cadena "ERROR" del comienzo
                rdr.Close();
            }
            catch (SqlException exc)
            {
                archivoZPLError.Replace("[ERROR]", exc.Message);
                nuevoArchivoZPL = archivoZPLError;
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                archivoZPLError.Replace("[ERROR]", exc.Message);
                nuevoArchivoZPL = archivoZPLError;
                Console.WriteLine(exc.Message);
            }
            try
            {
                archivoZPL = archivoZPL.Replace("[@definicionescuartos.sDescripcion;FI5@]", definicionCuarto);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion5@]", trad5);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion2@]", trad2);
                archivoZPL = archivoZPL.Replace("[@Mercaderias.sDescSenasa@]", descSenasa);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion1@]", trad1);
                //archivoZPL = archivoZPL.Replace("[@identificadores.dFechaProduccion;FFdd/MM/yyyy@]", trad5);
                //archivoZPL = archivoZPL.Replace("[@FechaVencimiento.dFecha;FFdd/MM/yyyy@]", trad5);
                archivoZPL = archivoZPL.Replace("[@Mercaderias.sEan@]", ean);
                archivoZPL = archivoZPL.Replace("[@Mercaderias.sCodigo@]", codigo);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion3@]", trad3); 
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion6@]", trad6);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion7@]", trad7);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion8@]", trad8);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion9@]", trad9);
                archivoZPL = archivoZPL.Replace("[@MercaderiasTraducciones.sDescripcion17@]", trad17);
                nuevoArchivoZPL = archivoZPL;
            }
            catch (NullReferenceException exc)
            { 
            // significa que paso algo, pero ya tendria que haberlo capturado antes
                Console.WriteLine(exc.Message);
                archivoZPLError.Replace("[ERROR]", exc.Message);
                nuevoArchivoZPL = archivoZPLError;
            }
            catch (Exception exc)
            {
                archivoZPLError.Replace("[ERROR]", exc.Message);
                nuevoArchivoZPL = archivoZPLError;
                Console.WriteLine(exc.Message);
            }
            return nuevoArchivoZPL;
        }

    }
}
