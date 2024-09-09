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
            CargaArticulos(_sistema);
            do
            {
                Console.WriteLine("1) Lista de cliente.\n2) Buscar articulo por categoria.\n3) Alta de articulo.\n4) Listar articulos - Prueba");
                switch (InputNumber("Seleccione una opcion ó 0 para salir", 4))
                {
                    case 2:
                        ListarArticulosXCat(_sistema);
                        break;
                    case 3:
                        AgregarArticulo(_sistema);
                        break;
                    case 4:
                        MostrarArticulos(_sistema);
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
            while (!exit);
        }
        public static void MostrarArticulos(Sistema sistema)
        {
            foreach (Articulo art in sistema.Articulos)
            {
                Console.WriteLine(art);
            }
        }

        public static void ListarArticulosXCat(Sistema sistema)
        {
            Console.Clear();
            Console.WriteLine("--- BUSQUEDA DE ARTICULOS POR CATEGORIA ---\n");
            int cont = 0;
            foreach (string cat in sistema.Categorias)
            {
                Console.WriteLine($"{++cont}) {cat}");
            }
            foreach(Articulo articulo in sistema.ObtenerArticulosXCat(sistema.Categorias[InputNumber("Seleccione una categoria", sistema.Categorias.Count) - 1]))
            {
                Console.WriteLine(articulo);
            }
            
        }

        public static void AgregarArticulo(Sistema sistema)
        {
            string nombre, categoria, codError;
            decimal precio;
            bool ok = false;
            codError = "";
            Console.Clear();
            Console.WriteLine("ALTA DE ARTICULO");
            nombre = InputString("Ingrese el nombre del articulo", 2);
            categoria = InputString("Ingrese la categoria del articulo", 5);
            precio = InputNumber("Ingrese el precio del articulo");
            do
            {
                try
                {
                    switch (codError)
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
                    }

                    sistema.AgregarArticulo(nombre, categoria, precio);
                    if (!sistema.Categorias.Contains(categoria))
                    {
                        sistema.AgregarCategoria(categoria);
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("--- Articulo ingresado correctamente ---");
                    Console.ResetColor();
                    ok = true;
                }
                catch (Exception error)
                {
                    codError = error.Message.Split(":")[0];
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error.Message.Split(":")[1]);
                    Console.ResetColor();
                }
            }
            while (!ok);
        }

        public static void CargaArticulos(Sistema sistema)
        {
            string[] nombre = new string[] {
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
        };

            string[] categoria = new string[] {
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
        };

            decimal[] precio = new decimal[] {
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
        };


            for (int i = 0; i < nombre.Length; i++)
            {
                sistema.AgregarArticulo(nombre[i], categoria[i], precio[i]);
                if (!sistema.Categorias.Contains(categoria[i]))
                {
                    sistema.AgregarCategoria(categoria[i]);
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"El valor ingresado debe tener al menos {condicionT} caracteres.");
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"El valor ingresado debe ser numerico.");
                    Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(aux ? error.Message : $"El valor ingresado debe ser numerico.");
                    Console.ResetColor();
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
                    Console.WriteLine(msj);
                    Console.ResetColor();
                    break;
                case "ERROR":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msj);
                    Console.ResetColor();
                    break;
            }
        }
    }
}
