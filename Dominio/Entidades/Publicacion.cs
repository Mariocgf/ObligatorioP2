namespace Dominio.Entidades
{
    public abstract class Publicacion
    {
        public enum Estado
        {
            ABIERTA = 0,
            CERRADA = 1,
            CANCELADA = 2
        }
        private static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado EstadoPublicacion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; } 
        public DateTime FechaTerminacionPublicacion { get; set; }
        private List<Articulo> _articulos;

        public List<Articulo> Articulos { get { return _articulos; } }

        public Publicacion(string nombre, Estado estado, DateTime fechaPublicacion, List<Articulo> articulos)
        {
            Id = ++IdCount;
            Nombre = nombre;
            EstadoPublicacion = estado;
            FechaPublicacion = fechaPublicacion;
            _articulos = articulos;
        }
        public virtual void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("E-NombreEmpty:El nombre ingresado esta vacio");
            }
        }
        public virtual void Validar()
        {
            ValidarNombre();
        }
        public abstract decimal Monto();
        public override string ToString()
        {
            string aux = "";
            foreach (Articulo articulo in _articulos)
            {
                aux += articulo.Nombre + " ";
            }
            return $"Publicacion {Id}\n" +
                $"Nombre: {Nombre}\n" +
                $"Estado: {EstadoPublicacion}\n" +
                $"Fecha de publicacion: {FechaPublicacion.ToString("d")}\n" +
                $"Articulos: {aux}";
        }
        public override bool Equals(object? obj)
        {
            Publicacion publicacion = obj as Publicacion;
            return publicacion != null && Nombre.ToLower() == publicacion.Nombre.ToLower();
        }
    }
}
