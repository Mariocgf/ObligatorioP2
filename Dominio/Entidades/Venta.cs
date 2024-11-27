namespace Dominio.Entidades
{
    public class Venta : Publicacion
    {
        public bool EnOfertaRelampago { get; set; }

        public Venta(string nombre, Estado estadoPublicacion, DateTime fechaPublicacion, List<Articulo> articulos, bool enOferta) : base(nombre, estadoPublicacion, fechaPublicacion, articulos)
        {
            EnOfertaRelampago = enOferta;
        }
        public override decimal Monto()
        {
            decimal total = base.Monto();
            if (EnOfertaRelampago)
            {
                total -= total * 0.20m;
            }
            return Math.Round(total, 2);
        }
        public override void CerrarVenta(Cliente cliente)
        {
            if (base.EstadoPublicacion != Estado.ABIERTA)
                throw new Exception("E-PublicacionCerrada:Esta publicacion ya esta cerrada.");
            if (Monto() > cliente.Billetera)
                throw new Exception("E-SaldoInsuficiente:Saldo insuficiente.");
            base.Cliente = cliente;
            base.Finalizador = cliente;
            base.EstadoPublicacion = Publicacion.Estado.CERRADA;
            base.FechaTerminacionPublicacion = DateTime.Now;
            cliente.Billetera -= Monto();
        }
        public override string ToString()
        {
            return $"" +
                $"{base.ToString()}\n" +
                $"Monto: {Monto()}\n" +
                $"En oferta: {(EnOfertaRelampago ? "Si" : "No")}\n";
        }
        public override void AgregarOferta(Oferta oferta) { }
        public override void FinalizarSubasta(Usuario finalizador) { }
        public override void Ofertar(Oferta oferta) { }
    }
}
