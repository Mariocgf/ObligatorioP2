using Dominio;
using Dominio.Entidades;
using System.Linq;

namespace App
{
    internal class Program
    {
        static Sistema _sistema = new Sistema();
        static void Main(string[] args)
        {
            bool exit = false;
            CargaArticulos();
            CargarPublicaciones();
            AgregarCliente();
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
                        MostrarClientes();
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
        public static void MostrarClientes()
        {
            foreach (Usuario usuario in _sistema.Usuarios)
            {
                Console.WriteLine(usuario);
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
                    Mensaje(error.Message.Split(":")[0], "ERROR");
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
        public static void AgregarCliente()
        {
            _sistema.AgregarUsuario("Juan", "Pérez", "juan.perez@example.com", "Contraseña123");
            _sistema.AgregarUsuario("Ana", "García", "ana.garcia@example.com", "Contraseña456");
            _sistema.AgregarUsuario("Carlos", "Martínez", "carlos.martinez@example.com", "Contraseña789");
            _sistema.AgregarUsuario("María", "López", "maria.lopez@example.com", "Contraseña321");
            _sistema.AgregarUsuario("Luis", "Gómez", "luis.gomez@example.com", "Contraseña654");
            _sistema.AgregarUsuario("Sofía", "Fernández", "sofia.fernandez@example.com", "Contraseña987");
            _sistema.AgregarUsuario("Miguel", "Sánchez", "miguel.sanchez@example.com", "Contraseña741");
            _sistema.AgregarUsuario("Laura", "Ramírez", "laura.ramirez@example.com", "Contraseña852");
            _sistema.AgregarUsuario("Diego", "Torres", "diego.torres@example.com", "Contraseña963");
            _sistema.AgregarUsuario("Lucía", "Vega", "lucia.vega@example.com", "Contraseña159");

            _sistema.ObtenerUsuario("juan.perez@example.com").Depositar(150);
            _sistema.ObtenerUsuario("ana.garcia@example.com").Depositar(500);
        }
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
        public static void CargarPublicaciones()
        {
            _sistema.AgregarPublicacion("Combo Oficina Moderna", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 5), [_sistema.ObtenerArticulo(1), _sistema.ObtenerArticulo(22), _sistema.ObtenerArticulo(32)], true);
            _sistema.AgregarPublicacion("Hogar Inteligente", Publicacion.Estado.CERRADA, new DateTime(2024, 10, 5), [_sistema.ObtenerArticulo(1), _sistema.ObtenerArticulo(2), _sistema.ObtenerArticulo(3)], false);
            _sistema.AgregarPublicacion("Cocina Funcional", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 6), [_sistema.ObtenerArticulo(6), _sistema.ObtenerArticulo(7), _sistema.ObtenerArticulo(5)], true);
            _sistema.AgregarPublicacion("Gimnasio en Casa", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 7), [_sistema.ObtenerArticulo(42), _sistema.ObtenerArticulo(43), _sistema.ObtenerArticulo(41)], true);
            _sistema.AgregarPublicacion("Rincón Acogedor", Publicacion.Estado.CERRADA, new DateTime(2024, 9, 25), [_sistema.ObtenerArticulo(24), _sistema.ObtenerArticulo(26), _sistema.ObtenerArticulo(29)], false);
            _sistema.AgregarPublicacion("Moda Deportiva", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 5), [_sistema.ObtenerArticulo(14), _sistema.ObtenerArticulo(15), _sistema.ObtenerArticulo(16)], true);
            _sistema.AgregarPublicacion("Accesorios para Viaje", Publicacion.Estado.CERRADA, new DateTime(2024, 9, 10), [_sistema.ObtenerArticulo(20), _sistema.ObtenerArticulo(19), _sistema.ObtenerArticulo(18)], false);
            _sistema.AgregarPublicacion("Entretenimiento en el Hogar", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 8), [_sistema.ObtenerArticulo(3), _sistema.ObtenerArticulo(38), _sistema.ObtenerArticulo(36)], true);
            _sistema.AgregarPublicacion("Música en Movimiento", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 12), [_sistema.ObtenerArticulo(47), _sistema.ObtenerArticulo(48), _sistema.ObtenerArticulo(50)], true);
            _sistema.AgregarPublicacion("Estudio Creativo", Publicacion.Estado.CERRADA, new DateTime(2024, 10, 10), [_sistema.ObtenerArticulo(33), _sistema.ObtenerArticulo(34), _sistema.ObtenerArticulo(35)], false);

            _sistema.AgregarPublicacion("Combo Oficina Moderna", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 5), [_sistema.ObtenerArticulo(1), _sistema.ObtenerArticulo(22), _sistema.ObtenerArticulo(32)]);



        }

        public static void RealizarOferta()
        {

        }

        public static void CargaArticulos()
        {
            string[] nombre = [
            "Laptop", "Smartphone", "Televisor", "Lavadora", "Refrigerador",
            "Microondas", "Tostadora", "Aspiradora", "Plancha", "Cafetera",
            "Reloj", "Anillo", "Collar", "Zapatos Deportivos", "Camiseta",
            "Chaqueta", "Pantalones", "Sombrero", "Gafas de sol", "Mochila",
            "Escritorio", "Silla", "Sofá", "Mesa de comedor", "Lámpara de pie",
            "Lámpara de mesa", "Alfombra", "Cuadro", "Espejo", "Vela Aromática",
            "Monitor", "Teclado", "Mouse", "Auriculares", "Altavoz Bluetooth",
            "Impresora", "Tablet", "Cámara", "Consola de videojuegos", "Bicicleta",
            "Patines", "Balón de fútbol", "Cinta para correr", "Pesas",
            "Batería Electrónica", "Guitarra", "Teclado Musical", "Bajo",
            "Batería Acústica", "Micrófono"
        ];

            string[] categoria = [
            "Electrónica", "Electrónica", "Electrónica", "Electrodoméstico", "Electrodoméstico",
            "Electrodoméstico", "Electrodoméstico", "Electrodoméstico", "Electrodoméstico", "Electrodoméstico",
            "Accesorios", "Joyería", "Joyería", "Ropa", "Ropa",
            "Ropa", "Ropa", "Accesorios", "Accesorios", "Accesorios",
            "Muebles", "Muebles", "Muebles", "Muebles", "Iluminación",
            "Iluminación", "Decoración", "Decoración", "Decoración", "Decoración",
            "Electrónica", "Electrónica", "Electrónica", "Electrónica", "Electrónica",
            "Electrónica", "Electrónica", "Electrónica", "Entretenimiento", "Deportes",
            "Deportes", "Deportes", "Deportes", "Deportes",
            "Música", "Música", "Música", "Música",
            "Música", "Música"
        ];

            decimal[] precio = [
            999.99m, 799.99m, 499.99m, 299.99m, 699.99m,
            89.99m, 29.99m, 159.99m, 39.99m, 79.99m,
            199.99m, 499.99m, 299.99m, 79.99m, 19.99m,
            49.99m, 39.99m, 24.99m, 99.99m, 59.99m,
            249.99m, 89.99m, 499.99m, 299.99m, 59.99m,
            39.99m, 99.99m, 149.99m, 79.99m, 19.99m,
            199.99m, 49.99m, 29.99m, 79.99m, 99.99m,
            149.99m, 299.99m, 599.99m, 399.99m, 249.99m,
            99.99m, 29.99m, 899.99m, 59.99m,
            799.99m, 299.99m, 499.99m, 399.99m,
            999.99m, 149.99m
        ];


            for (int i = 0; i < nombre.Length; i++)
            {
                _sistema.AgregarArticulo(nombre[i], categoria[i], precio[i]);
                if (!_sistema.Categorias.Contains(categoria[i]))
                {
                    _sistema.AgregarCategoria(categoria[i]);
                }

            }
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
