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

        public virtual void Validar()
        {
            
        }
        public abstract decimal Monto();


        public override string ToString()
        {
            return $"Publicacion {Id}\n" +
                $"Nombre: {Nombre}\n" +
                $"Estado: {EstadoPublicacion}\n" +
                $"Fecha de publicacion: {FechaPublicacion.ToString("d")}";
        }
    }
}
