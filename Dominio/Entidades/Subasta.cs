namespace Dominio.Entidades
{
    public class Subasta : Publicacion
    {
        public Oferta Oferta { get; set; }

        public override decimal Monto()
        {
            throw new NotImplementedException();
        }
        public Subasta(string nombre, Estado estado, DateTime fechaPublicacion, List<Articulo> articulos) : base(nombre, estado, fechaPublicacion, articulos)
        {
        }

        
    }
}
