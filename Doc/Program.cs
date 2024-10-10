using System.Text;
using System.IO;

namespace Doc
{
    /// <summary>
    /// Para utilizar este código:
    /// 1. Agregar un proyecto aplicación de consola en la solución que tiene el código fuente a imprimir
    /// 2. Configurar el proyecto de consola como proyecto de inicio
    /// 3. Ejecutar la aplicación
    /// 4. Archivos generados
    /// -> fuentesCs.txt: contiene el código fuente de los archivos .cs
    /// -> views.txt: contiene el código fuente de las vistas .cshtml
    /// -> precargas.txt: contiene los métodos de precarga de la clase Sistema.cs, es importante que los metodos 
    /// tenga la palabra 'Precargar' en su nombre y este diferencia los casos de exito y error con la palabra 'casos'
    /// </summary>
    internal class ImpresionCodigoFuenteSolucion
    {
        static void Main(string[] args)
        {
            Imprimir("*.cs", "fuentesCs.txt");
            
            Imprimir("*.cshtml", "views.txt");

            ImprimirPrecargas("Sistema.cs", "precargas.txt", "private void Precargar", "casos");

        }
        /// <summary>
        /// Imprime los archivos de código fuente de la solución
        /// </summary>
        /// <param name="tipoArchivo">El nombre del archivo a imprimir.Para imprimir todos los de un tipo determinado usar "*.extensión" por ejemplo:  "*.cs"</param>
        /// <param name="nombreArchivoSalida">El nombre del archivo de texto donde quedarà el código fuente</param>
        /// <remarks>
        /// Este código funciona siempre que el archivo de la solución (.sln) esté en la raíz de la solución,
        /// es decir cuando todos los proyectos estàn en subcarpetas de la carpeta de la solución
        /// </remarks>
        private static void Imprimir(string tipoArchivo, string nombreArchivoSalida, bool soloPrecargas = false)
        {
            try
            {
                string raizSolucion = ObtenerRutaSolucion();
                var separador = "***********************************" + Environment.NewLine;

                var archivos = System.IO.Directory.GetFiles(raizSolucion, tipoArchivo, System.IO.SearchOption.AllDirectories);

                //se obtienen los archivos .cs excluyendo los que contienen código generado por el framework
                var resultado = archivos.Where(
                    p => !p.Contains("Temporary")
                && !p.Contains("AssemblyInfo.cs")
                && !p.Contains("Program.cs")
                    && !p.Contains("AssemblyAttributes")
                    && !p.Contains(".g.cs"))
                    .Select(path => new { Carpeta = path, Nombre = System.IO.Path.GetFileName(path), Contenido = System.IO.File.ReadAllText(path) })
                            .Select(info =>
                                      separador
                                    + "Archivo: " + info.Nombre + Environment.NewLine
                                    + "Carpeta: " + info.Carpeta + Environment.NewLine
                                    + separador
                                    + info.Contenido);


                var concatenado = string.Join(Environment.NewLine, resultado);
                File.WriteAllText(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())))
                    + @$"\{nombreArchivoSalida}", concatenado, Encoding.UTF8);
            }
            catch (Exception algunError)
            {
                Console.WriteLine(algunError.Message);
            }
        }

        static string ObtenerRutaSolucion()
        {
            string directorioActivo = Directory.GetCurrentDirectory();

            Console.WriteLine("Directorio activo: " + directorioActivo);

            // Navega hacia arriba en la estructura de directorios hasta encontrar la carpeta de la solución
            DirectoryInfo directoryInfo = new DirectoryInfo(directorioActivo);

            while (directoryInfo != null && !DirectorioIncluye(directoryInfo, "*.sln"))
            {
                directoryInfo = directoryInfo.Parent;
            }

            if (directoryInfo != null)
            {
                return directoryInfo.FullName;
            }
            else
            {
                return string.Empty;
            }
        }

        static bool DirectorioIncluye(DirectoryInfo directory, string pattern)
        {
            return directory.GetFiles(pattern).Length > 0;
        }

        /// <summary>
        /// Imprime los metdos de precarga que estan en sistema
        /// </summary>
        /// <param name="nombreArchivo">El nombre del archivo deonde estas las precargas, por lo general sistema"</param>
        /// <param name="nombreArchivoSalida">El nombre del archivo de texto donde quedarà el código fuente</param>
        /// <param name="metodos">El nombre del metodos de las precargas</param>
        /// <param name="casos">Diferencia las datos de prueba al caso de uso exitos y alternativos (fallidos) en este ultimo es importante agregar porque falla</param>
        /// <remarks>
        /// </remarks>
        private static void ImprimirPrecargas(string nombreArchivo, string nombreArchivoSalida, string metodos, string casos)
        {
            try
            {
                string raizSolucion = ObtenerRutaSolucion();
                var separador = "***********************************" + Environment.NewLine;
                var separadorLinea = "-----------------------------------" + Environment.NewLine;

                var archivos = System.IO.Directory.GetFiles(raizSolucion, "*.cs", System.IO.SearchOption.AllDirectories);

                // Filtrar para encontrar el archivo 'sistema.cs'
                var archivoSistema = archivos.FirstOrDefault(p => p.EndsWith(nombreArchivo));

                if (archivoSistema != null)
                {
                    // Leer el contenido del archivo 'sistema.cs'
                    var contenidoSistema = System.IO.File.ReadAllLines(archivoSistema);

                    // Buscar y extraer los métodos que empiezan con "Precargar"
                    var metodosPrecargar = new List<string>();
                    var enMetodo = false;
                    var metodo = new StringBuilder();

                    foreach (var linea in contenidoSistema)
                    {
                        // Inicio de un nuevo método
                        if (linea.Trim().Contains(metodos) && !enMetodo)
                        {
                            enMetodo = true;
                            metodo.AppendLine(separador);
                            metodo.AppendLine(linea.Trim());
                            metodo.AppendLine(separador);
                        }
                        // Fin del método actual
                        else if (enMetodo && linea.Trim().StartsWith("}"))
                        {
                            enMetodo = false;
                            metodo.AppendLine(linea.Trim());
                            metodosPrecargar.Add(metodo.ToString());
                            metodo.Clear();
                        }
                        // Dentro del cuerpo del método
                        else if (enMetodo)
                        {
                            if (linea.Trim().Contains(casos))
                            {
                                metodo.AppendLine(separadorLinea);
                                metodo.AppendLine(linea.Trim());
                                metodo.AppendLine(separadorLinea);
                            }
                            else
                            {
                                metodo.AppendLine(linea.Trim());
                            }
                        }
                    }

                    // Crear el contenido a escribir en el archivo de salida
                    var resultado = string.Join(Environment.NewLine, metodosPrecargar);

                    // Escribir en el archivo de salida
                    File.WriteAllText(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))) + @$"\{nombreArchivoSalida}", resultado, Encoding.UTF8);
                }
                else
                {
                    Console.WriteLine($"El archivo {nombreArchivo} no fue encontrado.");
                }
            }
            catch (Exception algunError)
            {
                Console.WriteLine(algunError.Message);
            }
        }

    }
}


