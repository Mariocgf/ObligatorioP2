using Dominio;
using Dominio.Entidades;

namespace App
{
    internal class Program
    {
        static Sistema _sistema = new Sistema();
        static void Main(string[] args)
        {
            bool exit = false;
            _sistema.Precarga();
            do
            {
                Console.WriteLine("" +
                    "1) Lista de cliente.\n" +
                    "2) Buscar articulo por categoria.\n" +
                    "3) Alta de articulo.\n" +
                    "4) Listar publicaciones por fecha\n" +
                    "5) Test listado de publicacion\n");
                switch (InputNumber("Seleccione una opcion ó 0 para salir", 5))
                {
                    case 1:
                        Console.Clear();
                        ListarClientes();
                        break;
                    case 2:
                        ListarArticulosXCat();
                        break;
                    case 3:
                        AgregarArticulo();
                        break;
                    case 4:
                        ListarPublicacionesXFecha();
                        break;
                    case 5:
                        Console.Clear();
                        MostrarPublicaciones();
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
            while (!exit);
        }
        // Metodos de Listar
        public static void MostrarArticulos()
        {
            foreach (Articulo art in _sistema.Articulos)
            {
                Console.WriteLine(art);
            }
        }
        public static void MostrarPublicaciones()
        {
            foreach (Publicacion publi in _sistema.Publicaciones)
            {
                Console.WriteLine(publi);
            }
        }
        public static void ListarClientes()
        {
            foreach (Cliente cliente in _sistema.Usuarios)
            {
                Console.WriteLine(cliente);
            }
        }
        public static void ListarPublicacionesXFecha()
        {
            string option;
            List<Publicacion> publicaciones;
            DateTime fechaDesde, fechaHasta;
            bool aux = false;
            Console.Clear();
            fechaDesde = InputDate("Ingrese la fecha 'desde', en formato dd/mm/yyyy");
            fechaHasta = InputDate("Ingrese la fecha 'hasta', en formato dd/mm/yyyy");
            option = "";
            do
            {
                try
                {
                    switch (option)
                    {
                        case "E-FechaDesdeInvalida":
                            fechaDesde = InputDate("Reingrese la fecha 'desde', en formato dd/mm/yyyy");
                            break;
                        case "E-FechaHastaInvalida":
                            fechaHasta = InputDate("Reingrese la fecha 'Hasta', en formato dd/mm/yyyy");
                            break;
                        case "E-RangoIncompatible":
                            Mensaje("La fecha 'desde' tiene que ser menor a la fecha 'hasta'", "WARNING");
                            fechaDesde = InputDate("Reingrese la fecha 'desde', en formato dd/mm/yyyy");
                            fechaHasta = InputDate("Reingrese la fecha 'hasta', en formato dd/mm/yyyy");
                            break;
                    }

                    if (fechaDesde == DateTime.MinValue)
                    {
                        throw new Exception("E-FechaDesdeInvalida:La Fecha 'desde' no es correcta, vuelva a ingresar una fecha correcta.");
                    }
                    if (fechaHasta == DateTime.MinValue)
                    {
                        throw new Exception("E-FechaHastaInvalida:La Fecha 'hasta' no es correcta, vuelva a ingresar una fecha correcta.");
                    }
                    if (fechaDesde > fechaHasta)
                    {
                        throw new Exception("E-RangoIncompatible:La fecha 'desde' no puede ser mayor a la fecha 'hasta'");
                    }

                    publicaciones = _sistema.ObtenerPublicacionesXFecha(fechaDesde, fechaHasta);
                    if (publicaciones.Count == 0)
                    {
                        Mensaje($"No se encontraron publicaciones en el rango {fechaDesde.Date} - {fechaHasta.Date}", "WARNING");
                        aux = true;
                    }
                    foreach (Publicacion publicacion in publicaciones)
                    {
                        Console.WriteLine(publicacion);
                    }
                    aux = true;
                }
                catch (Exception error)
                {
                    option = error.Message.Split(":")[0];
                    Mensaje(error.Message.Split(":")[1], "ERROR");
                }

            }
            while (!aux);
        }
        public static void ListarArticulosXCat()
        {
            Mensaje("BUSQUEDA DE ARTICULOS POR CATEGORIA", "INICIO");
            int cont = 0;
            foreach (string cat in _sistema.Categorias)
            {
                Console.WriteLine($"{++cont}) {cat}");
            }
            foreach (Articulo articulo in _sistema.ObtenerArticulosXCat(_sistema.Categorias[InputNumber("Seleccione una categoria", _sistema.Categorias.Count) - 1]))
            {
                Console.WriteLine(articulo);
            }

        }

        // Metodo de alta
        public static void AgregarArticulo()
        {
            string nombre, categoria, codeError;
            decimal precio;
            bool ok = false;
            codeError = string.Empty;
            Mensaje("ALTA DE ARTICULO", "INICIO");
            nombre = InputString("Ingrese el nombre del articulo", 2);
            categoria = InputString("Ingrese la categoria del articulo", 5);
            precio = InputNumber("Ingrese el precio del articulo");
            do
            {
                try
                {
                    if (!string.IsNullOrEmpty(codeError))
                    {
                        switch (codeError)
                        {
                            case "E-CaracterNombre":
                                nombre = InputString("Ingrese el nombre del articulo", 2);
                                break;
                            case "E-CaracterCategoria":
                                categoria = InputString("Ingrese la categoria del articulo", 5);
                                break;
                            case "E-PrecioNeg":
                                precio = InputNumber("Ingrese el precio del articulo");
                                break;
                            case "E-ArticuloExistente":
                                ok = true;
                                break;
                        }
                        codeError = string.Empty;
                    }
                    _sistema.AgregarArticulo(nombre, categoria, precio);
                    _sistema.AgregarCategoria(categoria);
                    Console.Clear();
                    Mensaje("Articulo ingresado correctamente", "OK");
                    ok = true;
                }
                catch (Exception error)
                {
                    codeError = error.Message.Split(":")[0];
                    Mensaje(error.Message.Split(":")[1], "ERROR");
                }
            }
            while (!ok);
        }

        public static void RealizarOferta()
        {

        }

        // Metodos de entrada
        public static string InputString(string msj, int condicionT) // condicionT = condicion de tamaño
        {
            string data;
            do
            {
                try
                {
                    Console.Write($"{msj}: ");
                    data = Console.ReadLine();
                    if (data.Length < condicionT)
                    {
                        throw new Exception();
                    }
                    return data;
                }
                catch (Exception)
                {
                    Mensaje($"El valor ingresado debe tener al menos {condicionT} caracteres.", "ERROR");
                }
            }
            while (true);
        }
        public static decimal InputNumber(string msj)
        {
            decimal data;
            do
            {
                try
                {
                    Console.Write($"{msj}: ");
                    data = decimal.Parse(Console.ReadLine());
                    return data;
                }
                catch (Exception)
                {
                    Mensaje("El valor ingresado debe ser numerico.", "ERROR");
                }
            }
            while (true);
        }
        public static int InputNumber(string msj, int tope)
        {
            int data;
            bool aux = false;
            do
            {
                try
                {
                    Console.Write($"{msj}: ");
                    data = int.Parse(Console.ReadLine());
                    if (data < 0 || data > tope)
                    {
                        aux = true;
                        throw new Exception($"El valor ingresado no esta dentro del rango 1 a {tope}.");
                    }
                    return data;
                }
                catch (Exception error)
                {
                    Mensaje(aux ? error.Message : $"El valor ingresado debe ser numerico.", "ERROR");
                }
            }
            while (true);
        }
        public static DateTime InputDate(string msj)
        {
            string fechaInput, option;
            DateTime fechaOut;
            bool aux = false;
            fechaInput = InputString(msj, 10);
            option = "";
            do
            {
                try
                {
                    switch (option)
                    {
                        case "E-Fecha":
                            fechaInput = InputString("Ingrese la fecha 'desde', en formato dd/mm/yyyy", 10);
                            break;
                    }

                    if (!DateTime.TryParse(fechaInput, out fechaOut))
                    {
                        throw new Exception("E-FechaFormato:El formato de la fecha esta incorrecto");
                    }

                    aux = true;
                    return fechaOut;
                }
                catch (Exception error)
                {
                    option = error.Message.Split(":")[0];
                    Mensaje(error.Message.Split(":")[1], "ERROR");
                }
            }
            while (!aux);
            return DateTime.MinValue;
        }
        public static void Mensaje(string msj, string tipo)
        {
            switch (tipo)
            {
                case "OK":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"--- {msj} ---");
                    Console.ResetColor();
                    break;
                case "WARNING":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"--- {msj} ---");
                    Console.ResetColor();
                    break;
                case "ERROR":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msj);
                    Console.ResetColor();
                    break;
                case "INICIO":
                    Console.Clear();
                    Console.WriteLine($"--- {msj} ---");
                    break;
            }
        }

    }
}
