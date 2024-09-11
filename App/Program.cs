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
            do
            {
                Console.WriteLine("" +
                    "1) Lista de cliente.\n" +
                    "2) Buscar articulo por categoria.\n" +
                    "3) Alta de articulo.\n" +
                    "4) Listar articulos - Prueba");
                switch (InputNumber("Seleccione una opcion ó 0 para salir", 4))
                {
                    case 2:
                        ListarArticulosXCat();
                        break;
                    case 3:
                        AgregarArticulo();
                        break;
                    case 4:
                        MostrarArticulos();
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
            _sistema.AgregarPublicacion()
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
        public static void Mensaje(string msj, string tipo)
        {
            switch (tipo)
            {
                case "OK":
                    Console.ForegroundColor = ConsoleColor.Green;
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
