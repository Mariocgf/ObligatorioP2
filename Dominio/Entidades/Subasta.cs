using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Subasta : Publicacion
    {
        private List<Oferta> _ofertas = new List<Oferta>();

        public override decimal Monto()
        {
            throw new NotImplementedException();
        }
        public Subasta(string nombre, string estado, DateTime fechaPublicacion, List<Articulo> articulos) : base(nombre, estado, fechaPublicacion, articulos)
        {
        }
    }
}
