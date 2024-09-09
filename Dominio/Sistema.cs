using Dominio.Entidades;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Articulo> _articulos = new List<Articulo>();
        private List<string> _categorias = new List<string>();

        public List<Usuario> Usuarios {  get { return _usuarios; } }
        public List<Publicacion> Publicaciones { get { return _publicaciones; } }
        public List<Articulo> Articulos { get { return _articulos; } }
        public List <string> Categorias { get { return _categorias; } }

        // Metodo de agregacion
        public void AgregarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }
        public void AgregarPublicacion(Publicacion publicacion)
        {
            _publicaciones.Add(publicacion);
        }
        public void AgregarArticulo (string nombre, string categoria, decimal precio)
        {
            Articulo.Validar(nombre, categoria, precio);
            _articulos.Add(new Articulo(nombre, categoria, precio));
        }
        public void AgregarCategoria(string categoria)
        {
            _categorias.Add(categoria);
        }

        // Metodos de busquedas
        public List<Articulo> ObtenerArticulosXCat(string categoria)
        {
            List<Articulo> articulos = new List<Articulo>();
            foreach(Articulo articulo in _articulos)
            {
                if(articulo.Categoria == categoria)
                {
                    articulos.Add(articulo);
                }
            }
            return articulos;
        }

    }
}
