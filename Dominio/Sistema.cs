using Dominio.Entidades;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Articulo> _articulos = new List<Articulo>();
        private List<string> _categorias = new List<string>();

        public List<Usuario> Usuarios { get { return _usuarios; } }
        public List<Publicacion> Publicaciones { get { return _publicaciones; } }
        public List<Articulo> Articulos { get { return _articulos; } }
        public List<string> Categorias { get { return _categorias; } }

        // Metodo de agregacion
        public void AgregarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }
        public void AgregarPublicacion(string nombre, string estado, DateTime fechaPublicacion, List<Articulo> articulos, bool enOferta)
        {
            if(articulos.Count == 0)
            {
                throw new Exception("E-PubliArticuloVacio:Ingrese articulos para crear la publicacion.");
            }
            Publicacion publicacion = new Venta(nombre, estado, fechaPublicacion, articulos, enOferta);
            if (publicacion == null)
            {
                throw new Exception("E-PubliInvalido:Publicacion invalida. Intente de nuevo.");
            }
            publicacion.Validar();
            _publicaciones.Add(publicacion);
        }
        public void AgregarPublicacion(string nombre, string estado, DateTime fechaPublicacion, List<Articulo> articulos)
        {
            if (articulos.Count == 0)
            {
                throw new Exception("E-PubliArticuloVacio:Ingrese articulos para crear la publicacion.");
            }
            Publicacion publicacion = new Subasta(nombre, estado, fechaPublicacion, articulos);
            if (publicacion == null)
            {
                throw new Exception("E-PubliInvalido:Publicacion invalida. Intente de nuevo.");
            }
            publicacion.Validar();
            _publicaciones.Add(publicacion);
        }
        public void AgregarArticulo(string nombre, string categoria, decimal precio)
        {
            Articulo articulo = new Articulo(nombre, categoria, precio);
            if(articulo == null)
            {
                throw new Exception("E-ArticuloInvalido:Articulo ingresado invalido. Intente de nuevo.");
            }
            articulo.Validar();
            if (_articulos.Contains(articulo))
            {
                throw new Exception("E-ArticuloExistente:El articulo ingresado ya existe, reingrese uno nuevo.");
            }
            _articulos.Add(articulo);
        }
        public void AgregarCategoria(string categoria)
        {
            if (!_categorias.Contains(categoria))
            {
                _categorias.Add(categoria);
            }
        }

        // Metodos de busquedas
        public List<Articulo> ObtenerArticulosXCat(string categoria)
        {
            List<Articulo> articulos = new List<Articulo>();
            foreach (Articulo articulo in _articulos)
            {
                if (articulo.Categoria == categoria)
                {
                    articulos.Add(articulo);
                }
            }
            return articulos;
        }
        public Articulo ObtenerArticulo(string nombre)
        {
            foreach(Articulo articulo in _articulos)
            {
                if (articulo.Nombre == nombre)
                {
                    return articulo;
                }
            }
            return null;
        }
        public Articulo ObtenerArticulo(int id)
        {
            foreach (Articulo articulo in _articulos)
            {
                if (articulo.Id == id)
                {
                    return articulo;
                }
            }
            return null;
        }
    }
}
