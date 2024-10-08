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
        public void AgregarUsuario(string nombre, string apellido, string email, string contrasenia)
        {
            _usuarios.Add(new Cliente(nombre, apellido, email, contrasenia));
        }
        public void AgregarPublicacion(string nombre, Publicacion.Estado estado, DateTime fechaPublicacion, List<Articulo> articulos, bool enOferta)
        {
            if (articulos.Count == 0)
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
        public void AgregarPublicacion(string nombre, Publicacion.Estado estado, DateTime fechaPublicacion, List<Articulo> articulos)
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
            if (articulo == null)
            {
                throw new Exception("E-ArticuloInvalido:Articulo ingresado invalido. Intente de nuevo.");
            }
            articulo.Validar();
            if (_articulos.Contains(articulo))
            {
                throw new Exception("E-ArticuloExistente:El articulo ingresado ya existe, reingrese uno nuevo.");
            }
            AgregarCategoria(categoria);
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
        public Usuario ObtenerUsuario(string email)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Email == email)
                {
                    return usuario;
                }
            }
            return null;
        }
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
            foreach (Articulo articulo in _articulos)
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
        public List<Publicacion> ObtenerPublicacionesXFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Publicacion> aux = new List<Publicacion>();
            foreach(Publicacion publicacion in _publicaciones)
            {
                if(publicacion.FechaPublicacion.Date > fechaDesde.Date && publicacion.FechaPublicacion.Date < fechaHasta.Date)
                {
                    aux.Add(publicacion);
                }
            }
            return aux;
        }
        public Publicacion ObtenerPublicacion(int id)
        {
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion.Id == id)
                {
                    return publicacion;
                }
            }
            return null;
        }
        public void Precarga()
        {
            CargarArticulos();
            CargarPublicaciones();
            CargarUsuario();
        }

        public void CargarPublicaciones()
        {
            AgregarPublicacion("Combo Oficina Moderna", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 5), [ObtenerArticulo(1),ObtenerArticulo(22), ObtenerArticulo(32)], true);
            AgregarPublicacion("Hogar Inteligente", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 5), [ObtenerArticulo(1), ObtenerArticulo(2), ObtenerArticulo(3)], false);
            AgregarPublicacion("Cocina Funcional", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 6), [ObtenerArticulo(6), ObtenerArticulo(7), ObtenerArticulo(5)], true);
            AgregarPublicacion("Gimnasio en Casa", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 7), [ObtenerArticulo(42), ObtenerArticulo(43), ObtenerArticulo(41)], true);
            AgregarPublicacion("Rincón Acogedor", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 25), [ObtenerArticulo(24), ObtenerArticulo(26), ObtenerArticulo(29)], false);
            AgregarPublicacion("Moda Deportiva", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 5), [ObtenerArticulo(14), ObtenerArticulo(15), ObtenerArticulo(16)], true);
            AgregarPublicacion("Accesorios para Viaje", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 10), [ObtenerArticulo(20), ObtenerArticulo(19), ObtenerArticulo(18)], false);
            AgregarPublicacion("Entretenimiento en el Hogar", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 8), [ObtenerArticulo(3), ObtenerArticulo(38), ObtenerArticulo(36)], true);
            AgregarPublicacion("Música en Movimiento", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 12), [ObtenerArticulo(47), ObtenerArticulo(48), ObtenerArticulo(50)], true);
            AgregarPublicacion("Estudio Creativo", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 10), [ObtenerArticulo(33), ObtenerArticulo(34), ObtenerArticulo(35)], false);

            AgregarPublicacion("Combo Oficina Moderna", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 5), [ObtenerArticulo(1), ObtenerArticulo(22), ObtenerArticulo(32)]);

        }

        public void CargarArticulos()
        {
            AgregarArticulo("Laptop", "Electrónica", 999.99m);
            AgregarArticulo("Smartphone", "Electrónica", 799.99m);
            AgregarArticulo("Televisor", "Electrónica", 499.99m);
            AgregarArticulo("Lavadora", "Electrodoméstico", 299.99m);
            AgregarArticulo("Refrigerador", "Electrodoméstico", 699.99m);
            AgregarArticulo("Microondas", "Electrodoméstico", 89.99m);
            AgregarArticulo("Tostadora", "Electrodoméstico", 29.99m);
            AgregarArticulo("Aspiradora", "Electrodoméstico", 159.99m);
            AgregarArticulo("Plancha", "Electrodoméstico", 39.99m);
            AgregarArticulo("Cafetera", "Electrodoméstico", 79.99m);
            AgregarArticulo("Reloj", "Accesorios", 199.99m);
            AgregarArticulo("Anillo", "Joyería", 499.99m);
            AgregarArticulo("Collar", "Joyería", 299.99m);
            AgregarArticulo("Zapatos Deportivos", "Ropa", 79.99m);
            AgregarArticulo("Camiseta", "Ropa", 19.99m);
            AgregarArticulo("Chaqueta", "Ropa", 49.99m);
            AgregarArticulo("Pantalones", "Ropa", 39.99m);
            AgregarArticulo("Sombrero", "Accesorios", 24.99m);
            AgregarArticulo("Gafas de sol", "Accesorios", 99.99m);
            AgregarArticulo("Mochila", "Accesorios", 59.99m);
            AgregarArticulo("Escritorio", "Muebles", 249.99m);
            AgregarArticulo("Silla", "Muebles", 89.99m);
            AgregarArticulo("Sofá", "Muebles", 499.99m);
            AgregarArticulo("Mesa de comedor", "Muebles", 299.99m);
            AgregarArticulo("Lámpara de pie", "Iluminación", 59.99m);
            AgregarArticulo("Lámpara de mesa", "Iluminación", 39.99m);
            AgregarArticulo("Alfombra", "Decoración", 99.99m);
            AgregarArticulo("Cuadro", "Decoración", 149.99m);
            AgregarArticulo("Espejo", "Decoración", 79.99m);
            AgregarArticulo("Vela Aromática", "Decoración", 19.99m);
            AgregarArticulo("Monitor", "Electrónica", 199.99m);
            AgregarArticulo("Teclado", "Electrónica", 49.99m);
            AgregarArticulo("Mouse", "Electrónica", 29.99m);
            AgregarArticulo("Auriculares", "Electrónica", 79.99m);
            AgregarArticulo("Altavoz Bluetooth", "Electrónica", 99.99m);
            AgregarArticulo("Impresora", "Electrónica", 149.99m);
            AgregarArticulo("Tablet", "Electrónica", 299.99m);
            AgregarArticulo("Cámara", "Electrónica", 599.99m);
            AgregarArticulo("Consola de videojuegos", "Entretenimiento", 399.99m);
            AgregarArticulo("Bicicleta", "Deportes", 249.99m);
            AgregarArticulo("Patines", "Deportes", 99.99m);
            AgregarArticulo("Balón de fútbol", "Deportes", 29.99m);
            AgregarArticulo("Cinta para correr", "Deportes", 899.99m);
            AgregarArticulo("Pesas", "Deportes", 59.99m);
            AgregarArticulo("Batería Electrónica", "Música", 799.99m);
            AgregarArticulo("Guitarra", "Música", 299.99m);
            AgregarArticulo("Teclado Musical", "Música", 499.99m);
            AgregarArticulo("Bajo", "Música", 399.99m);
            AgregarArticulo("Batería Acústica", "Música", 999.99m);
            AgregarArticulo("Micrófono", "Música", 149.99m);
        }
        public void CargarUsuario()
        {
            AgregarUsuario("Juan", "Pérez", "juan.perez@example.com", "Contraseña123");
            AgregarUsuario("Ana", "García", "ana.garcia@example.com", "Contraseña456");
            AgregarUsuario("Carlos", "Martínez", "carlos.martinez@example.com", "Contraseña789");
            AgregarUsuario("María", "López", "maria.lopez@example.com", "Contraseña321");
            AgregarUsuario("Luis", "Gómez", "luis.gomez@example.com", "Contraseña654");
            AgregarUsuario("Sofía", "Fernández", "sofia.fernandez@example.com", "Contraseña987");
            AgregarUsuario("Miguel", "Sánchez", "miguel.sanchez@example.com", "Contraseña741");
            AgregarUsuario("Laura", "Ramírez", "laura.ramirez@example.com", "Contraseña852");
            AgregarUsuario("Diego", "Torres", "diego.torres@example.com", "Contraseña963");
            AgregarUsuario("Lucía", "Vega", "lucia.vega@example.com", "Contraseña159");
        }
    }
}
