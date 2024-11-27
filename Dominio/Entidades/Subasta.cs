namespace Dominio.Entidades
{
    public class Subasta : Publicacion
    {
        private List<Oferta> _listaOferta = new List<Oferta>();

        public List<Oferta> ListaOferta { get { return _listaOferta; } }


        public Subasta(string nombre, Estado estado, DateTime fechaPublicacion, List<Articulo> articulos) : base(nombre, estado, fechaPublicacion, articulos)
        {
        }
        public override void AgregarOferta(Oferta oferta)
        {
            _listaOferta.Insert(0, oferta);
        }
        public override void CerrarVenta(Cliente cliente) { }
        public override void Ofertar(Oferta oferta)
        {
            oferta.Verificar();
            if (_listaOferta.Count() > 0 && oferta.Monto <= _listaOferta[0].Monto)
                throw new Exception("El monto ofertado es menor o igual al mayor postor.");
            if (oferta.Monto < base.Monto())
                throw new Exception("El monto es menor al precio base");
            AgregarOferta(oferta);
        }
        public override void FinalizarSubasta(Usuario finalizador)
        {
            foreach (Oferta oferta in _listaOferta)
            {
                if (oferta.Cliente.Billetera >= oferta.Monto)
                {
                    base.EstadoPublicacion = Publicacion.Estado.CERRADA;
                    base.Cliente = oferta.Cliente;
                    base.Finalizador = finalizador;
                    base.Cliente.Billetera -= oferta.Monto;
                    break;
                }
            }
            if (base.Cliente is null)
                base.EstadoPublicacion = Publicacion.Estado.CANCELADA;
            base.FechaTerminacionPublicacion = DateTime.Now;
        }
    }
}
