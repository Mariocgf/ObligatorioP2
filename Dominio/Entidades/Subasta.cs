namespace Dominio.Entidades
{
    public class Subasta : Publicacion
    {
        private List<Oferta> _ofertas = new List<Oferta>();

        public override decimal Monto()
        {
            throw new NotImplementedException();
        }
        public Subasta(string nombre, Estado estado, DateTime fechaPublicacion, List<Articulo> articulos) : base(nombre, estado, fechaPublicacion, articulos)
        {
        }

        public override Oferta UltimaOferta()
        {
            decimal max = 0;
            Oferta aux = null;
            foreach (Oferta oferta in _ofertas)
            {
                if (oferta.Monto > max)
                {
                    max = oferta.Monto;
                    aux = oferta;
                }
            }
            if (max != 0)
            {
                return aux;
            }
            else
            {
                return null;
            }
            
        }
    }
}
