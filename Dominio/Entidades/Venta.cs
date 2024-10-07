namespace Dominio.Entidades
{
    public class Venta : Publicacion
    {
        public bool EnOfertaRelampago { get; set; }

        public Venta(string nombre, Estado estado, DateTime fechaPublicacion, List<Articulo> articulos, bool enOferta) : base(nombre, estado, fechaPublicacion, articulos)
        {
            EnOfertaRelampago = enOferta;
        }
        public override decimal Monto()
        {
            decimal total = 0;
            foreach (Articulo articulo in base.Articulos)
            {
                total += articulo.Precio;
            }
            if (EnOfertaRelampago)
            {
                return total - total * 0.20m;
            }
            return total;
        }

        public override string ToString()
        {
            return $"" +
                $"{base.ToString()}\n" +
                $"Monto: {Monto()}\n" +
                $"En oferta: {(EnOfertaRelampago ? "Si" : "No")}\n";
        }

        public override Oferta UltimaOferta()
        {
            throw new NotImplementedException();
        }
    }
}
