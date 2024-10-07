namespace Dominio.Entidades
{
    public class Oferta
    {
        private static int s_idCont;
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Oferta (Usuario usuario, decimal monto, DateTime fecha)
        {
            Id = ++ s_idCont;
            Usuario = usuario;
            Monto = monto;
            Fecha = fecha;
        }
    }
}
