namespace Dominio.Entidades
{
    public abstract class Publicacion
    {
        private static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPublicacion { get; set; }
        private List<Articulo> _articulos;

        public List<Articulo> Articulos { get { return _articulos; } }


        public Publicacion(string nombre, string estado, DateTime fechaPublicacion, List<Articulo> articulos)
        {
            Id = ++IdCount;
            Nombre = nombre;
            Estado = estado;
            FechaPublicacion = fechaPublicacion;
            _articulos = articulos;
        }
        private void ValidarEstado(string estado)
        {
            string[] estados = ["ABIERTA", "CERRADA", "CANCELADA"];
            if (!estados.Contains(estado.ToUpper()))
            {
                throw new Exception("E-EstadoInvalido:El estado ingresado es invalido.");
            }
        }
        public virtual void Validar()
        {
            ValidarEstado(Estado);
        }
        public abstract decimal Monto();

        public override string ToString()
        {
            return $"Publicacion {Id}\n" +
                $"Nombre: {Nombre}\n" +
                $"Estado: {Estado}\n" +
                $"Fecha de publicacion: {FechaPublicacion.ToString("d")}";
        }
    }
}
