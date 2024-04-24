using System;
using System.IO;
using System.Text;

namespace QuieroImprimirChino
{
    class LeerArchivoTexto
    {
        // lo separo en una clase aparte para manejar todas las excepciones propias de levantar un archivo
        // esta clase la utilizo para abrir un archivo de texto del filesystem
        public String textoZPL;
        private String mensaje;
        
        public String leerError()
        {
            // encapsulo el mensaje de error
            if (mensaje == null)
                mensaje = "";
            return mensaje;
        }
        public String cargar(String archivoFormato) {
            mensaje = "";
            if (archivoFormato == null)
                mensaje = "No se proveyó una ruta y nombre de archivo (nulo)";
            else
            {
                try
                {
                    textoZPL = System.IO.File.ReadAllText(archivoFormato);
                }
                catch (UnauthorizedAccessException)
                {
                    mensaje = "Al tratar de leer el archivo " + archivoFormato + " no cuenta con suficientes permisos o la carpeta no existe";
                }
                catch (FileNotFoundException)
                {
                    mensaje = "El archivo o el directorio no existen (" + archivoFormato + ").";
                }
                catch (DirectoryNotFoundException)
                {
                    mensaje = "El directorio no existe (" + archivoFormato + ").";
                }
                catch (DriveNotFoundException)
                {
                    mensaje = "La ruta (unidad) no existe  (" + archivoFormato + ").";
                }
                catch (PathTooLongException)
                {
                    mensaje = "La ruta excede el largo maximo admible  (" + archivoFormato + ").";
                }
                catch (IOException e)
                {
                    if ((e.HResult & 0x0000FFFF) == 32)
                        mensaje = "Error sharing violation  (" + archivoFormato + ").";
                    else
                        mensaje = "Excepcion ocurrida:\nError code: " + e.Message + "(" + archivoFormato + ").";
                }
            }
            return mensaje;
        }
        public void reemplazarTexto(String buscar, String reemplazar)
        {
            if (textoZPL.Contains(buscar))
                textoZPL.Replace(buscar, reemplazar);
            else
                mensaje = "No se encontro la cadena " + buscar + " en el texto";
        }
   
    }
}
