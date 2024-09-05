using Dominio.Entidades;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios;
        private List<Publicacion> _publicaciones;
        private List<Articulo> _articulos = new List<Articulo>();

        public void AgregarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }
        public void AgregarPublicaciones(Publicacion publicaciones)
        {
            _publicaciones.Add(publicaciones);
        }
        public void AgregarArticulos (Articulo articulo)
        {
            _articulos.Add(articulo);
        }
        public List<Articulo> DevolverArticulos()
        {
            return _articulos;
        }
    }
}
