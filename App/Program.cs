using Dominio;
using Dominio.Entidades;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sistema sistema = new Sistema();
            CargaArticulos(sistema);
            foreach (Articulo item in sistema.DevolverArticulos())
            {
                Console.WriteLine(item);
            }
        }

        public static void CargaArticulos(Sistema sistema)
        {
            Articulo[] articulos = new Articulo[]
            {
                new Articulo("Laptop", "Electrónica", 999.99m),
                new Articulo("Smartphone", "Electrónica", 699.99m),
                new Articulo("Sofá", "Muebles", 549.99m),
                new Articulo("Cafetera", "Electrodomésticos", 89.99m),
                new Articulo("Libro", "Libros", 19.99m),
                new Articulo("Camisa", "Ropa", 29.99m),
                new Articulo("Bicicleta", "Deportes", 399.99m),
                new Articulo("Televisor", "Electrónica", 1199.99m),
                new Articulo("Escritorio", "Muebles", 199.99m),
                new Articulo("Auriculares", "Electrónica", 149.99m)
            };

            foreach (Articulo item in articulos)
            {
                sistema.AgregarArticulos(item);
            }
        }
    }
}
