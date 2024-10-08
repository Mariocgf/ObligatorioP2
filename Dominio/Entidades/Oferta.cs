namespace Dominio.Entidades
{
    public class Oferta
    {
        private static int s_idCont;
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Oferta (Cliente cliente, decimal monto, DateTime fecha)
        {
            Id = ++ s_idCont;
            Cliente = cliente;
            Monto = monto;
            Fecha = fecha;
        }
    }
}
