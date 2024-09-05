namespace Dominio.Entidades
{
    public class Articulo
    {
        public static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }

        public Articulo (string nombre, string categoria, decimal precio)
        {
            Id = ++IdCount;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nNombre: {Nombre}\nCategoria: {Categoria}\nPrecio: {Precio}";
        }
    }
}
