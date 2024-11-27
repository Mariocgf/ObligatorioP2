namespace Dominio.Entidades
{
    public class Oferta
    {
        private static int s_idCont;
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public Oferta(Cliente cliente, decimal monto, DateTime fecha)
        {
            Id = ++s_idCont;
            Cliente = cliente;
            Monto = monto;
            Fecha = fecha;
        }
        public void Verificar()
        {
            if (Monto <= 0)
                throw new Exception("El monto ofertado es nulo o negativo");
            if (Cliente == null)
                throw new Exception("El cliente es nulo");
        }
    }
}
