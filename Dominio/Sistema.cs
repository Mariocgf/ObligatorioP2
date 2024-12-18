﻿using Dominio.Entidades;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Articulo> _articulos = new List<Articulo>();
        private List<string> _categorias = new List<string>();
        private static Sistema _instancia;

        public List<Usuario> Usuarios { get { return _usuarios; } }
        public IEnumerable<Publicacion> Publicaciones { get { return _publicaciones; } }
        public List<Articulo> Articulos { get { return _articulos; } }
        public List<string> Categorias { get { return _categorias; } }
        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia;
            }
        }

        private Sistema()
        {
            Precarga();
        }

        // Metodo de agregacion
        public void AgregarCliente(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("E-UsuarioNull:El usuario ingresado es nulo.");
            }
            usuario.Validar();
            if (_usuarios.Contains(usuario))
            {
                throw new Exception("E-UsuarioExistente:El usuario ingresado ya esta registrado");
            }
            _usuarios.Add(usuario);
        }
        public void AgregarAdministrador(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("E-UsuarioNull:El usuario ingresado es nulo.");
            }
            usuario.Validar();
            if (_usuarios.Contains(usuario))
            {
                throw new Exception("E-UsuarioExistente:El usuario ingresado ya esta registrado");
            }
            _usuarios.Add(usuario);
        }
        public void AgregarPublicacion(string nombre,
                                        Publicacion.Estado estado,
                                        DateTime fechaPublicacion,
                                        List<Articulo> articulos,
                                        bool enOferta)
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
            if (_publicaciones.Contains(publicacion))
            {
                throw new Exception("E-PublicacionExistente:La publicacion ingresada ya esta registrada");
            }
            publicacion.Validar();
            _publicaciones.Add(publicacion);
        }
        public void AgregarPublicacion(string nombre,
                                        Publicacion.Estado estado,
                                        DateTime fechaPublicacion,
                                        List<Articulo> articulos)
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
            if (_publicaciones.Contains(publicacion))
            {
                throw new Exception("E-PublicacionExistente:La publicacion ingresada ya esta registrada");
            }
            publicacion.Validar();
            _publicaciones.Add(publicacion);
        }
        public void AgregarArticulo(string nombre,
                                     string categoria,
                                     decimal precio)
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
        public Usuario ObtenerUsuario(int id)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Id == id)
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
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion.FechaPublicacion.Date > fechaDesde.Date && publicacion.FechaPublicacion.Date < fechaHasta.Date)
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

        //Obligatorio 2
        public IEnumerable<Publicacion> ObtenerPublicacionesVentas()
        {
            List<Publicacion> aux = new List<Publicacion>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Venta)
                {
                    aux.Add(publicacion);
                }
            }
            return aux;
        }
        public IEnumerable<Publicacion> ObtenerPublicacionesSubasta()
        {
            List<Publicacion> aux = new List<Publicacion>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Subasta)
                {
                    aux.Add(publicacion);
                }
            }
            aux.Sort();
            return aux;
        }
        public Venta? ObtenerPublicacionVenta(int id)
        {
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Venta && publicacion.Id == id)
                    return (Venta)publicacion;
            }
            return null;
        }
        public Subasta? ObtenerPublicacionSubasta(int id)
        {
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Subasta && publicacion.Id == id)
                    return (Subasta)publicacion;
            }
            return null;
        }
        public bool Login(string email, string contrasenia)
        {
            Usuario user = ObtenerUsuario(email) ?? throw new Exception("E-UsuarioNoExistente:El usuario no existe.");
            if (user.Contrasenia != contrasenia)
            {
                throw new Exception("E-ContraseniaIncorrecta:La contraseña ingresada no es correcta.");
            }
            return true;
        }
        public void Comprar(int idP, string email)
        {
            Venta? publicacion = ObtenerPublicacionVenta(idP);
            Cliente? cliente = ObtenerUsuario(email) is Cliente ? (Cliente)ObtenerUsuario(email) : null;
            if (cliente == null)
                throw new Exception("E-CompraCliente:Solo el cliente puede realizar la compra");
            if (publicacion == null)
                throw new Exception("E-PublicacionNF:La publicacion a comprar no se encontro");
            publicacion.CerrarVenta(cliente);
        }
        public void Ofertar(int id, Cliente cliente, decimal monto)
        {
            Subasta? publicacion = ObtenerPublicacionSubasta(id);
            Oferta oferta = new Oferta(cliente, monto, DateTime.Now);
            if (publicacion == null)
                throw new Exception("E-PublicacionNF:La publicacion a comprar no se encontro");
            publicacion.Ofertar(oferta);
        }
        public void FinalizarSubasta(int id, Usuario finalizador)
        {
            Subasta? subasta = ObtenerPublicacionSubasta(id);
            if (subasta == null)
                throw new Exception("E-SubastaNF:Subasta no encontrada");
            subasta.FinalizarSubasta(finalizador);
        }

        private void Precarga()
        {
            PrecargarArticulos();
            PrecargarUsuario();
            PrecargarPublicaciones();
        }
        private void PrecargarPublicaciones()
        {
            //  Casos de exito
            AgregarPublicacion("Combo Oficina Moderna", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 15), [ObtenerArticulo(7), ObtenerArticulo(12), ObtenerArticulo(17)], true);
            AgregarPublicacion("Kit de Entretenimiento Electrónico", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 5), [ObtenerArticulo(6), ObtenerArticulo(24), ObtenerArticulo(35)], false);
            AgregarPublicacion("Paquete de Dormitorio Completo", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 27), [ObtenerArticulo(5), ObtenerArticulo(4), ObtenerArticulo(3)], true);
            AgregarPublicacion("Set Fotografía Profesional", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 2), [ObtenerArticulo(3), ObtenerArticulo(8), ObtenerArticulo(28)], false);
            AgregarPublicacion("Kit de Cocina Completo", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 22), [ObtenerArticulo(26), ObtenerArticulo(30), ObtenerArticulo(27)], true);
            AgregarPublicacion("Combo Tecnología para Oficina", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 18), [ObtenerArticulo(7), ObtenerArticulo(21), ObtenerArticulo(2)], false);
            AgregarPublicacion("Accesorios para Viaje", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 3), [ObtenerArticulo(49), ObtenerArticulo(43), ObtenerArticulo(46)], true);
            AgregarPublicacion("Sala Moderna y Minimalista", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 30), [ObtenerArticulo(14), ObtenerArticulo(15), ObtenerArticulo(13)], false);
            AgregarPublicacion("Electrodomésticos de Cocina", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 1), [ObtenerArticulo(26), ObtenerArticulo(23), ObtenerArticulo(28)], true);
            AgregarPublicacion("Paquete de Audio y Entretenimiento", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 25), [ObtenerArticulo(9), ObtenerArticulo(19), ObtenerArticulo(6)], false);

            AgregarPublicacion("Pack de Trabajo en Casa", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 12), [ObtenerArticulo(5), ObtenerArticulo(1), ObtenerArticulo(18)]);
            AgregarPublicacion("Conjunto de Muebles para Sala", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 4), [ObtenerArticulo(15), ObtenerArticulo(13), ObtenerArticulo(17)]);
            AgregarPublicacion("Kit de Viaje y Aventura", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 28), [ObtenerArticulo(46), ObtenerArticulo(49), ObtenerArticulo(44)]);
            AgregarPublicacion("Paquete Básico de Electrónica", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 6), [ObtenerArticulo(1), ObtenerArticulo(2), ObtenerArticulo(5)]);
            AgregarPublicacion("Set de Decoración Moderna", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 20), [ObtenerArticulo(19), ObtenerArticulo(16), ObtenerArticulo(18)]);
            AgregarPublicacion("Paquete Oficina y Tecnología", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 24), [ObtenerArticulo(7), ObtenerArticulo(12), ObtenerArticulo(6)]);
            AgregarPublicacion("Electrodomésticos para el Hogar", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 26), [ObtenerArticulo(25), ObtenerArticulo(24), ObtenerArticulo(27)]);
            AgregarPublicacion("Tecnología y Entretenimiento Familiar", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 2), [ObtenerArticulo(35), ObtenerArticulo(39), ObtenerArticulo(40)]);
            AgregarPublicacion("Set Completo de Cocina", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 29), [ObtenerArticulo(28), ObtenerArticulo(29), ObtenerArticulo(30)]);
            AgregarPublicacion("Paquete de Iluminación y Decoración", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 7), [ObtenerArticulo(14), ObtenerArticulo(16), ObtenerArticulo(20)]);

            // Casos de error
            //AgregarPublicacion("Sala Moderna y Minimalista", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 30), [ObtenerArticulo(14), ObtenerArticulo(15), ObtenerArticulo(13)], false); La publicacion ingresada ya esta registrada -> venta
            //AgregarPublicacion("Paquete de Iluminación y Decoración", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 7), [ObtenerArticulo(14), ObtenerArticulo(16), ObtenerArticulo(20)]); La publicacion ingresada ya esta registrada -> subasta
            //AgregarPublicacion("", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 29), [ObtenerArticulo(28), ObtenerArticulo(29), ObtenerArticulo(30)]); El nombre ingresado esta vacio -> subasta
            //AgregarPublicacion("Kit de Cocina Completo", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 22), [ObtenerArticulo(26), ObtenerArticulo(30), ObtenerArticulo(27)], true); La publicacion ingresada ya esta registrada -> venta
            //AgregarPublicacion("Electrodomésticos de Cocina", Publicacion.Estado.ABIERTA, new DateTime(2024, 10, 1), [], true); Ingrese articulos para crear la publicacion -> venta
            //AgregarPublicacion("Paquete Oficina y Tecnología", Publicacion.Estado.ABIERTA, new DateTime(2024, 9, 24), []); Ingrese articulos para crear la publicacion -> subasta


        }
        private void PrecargarArticulos()
        {
            // Casos de exitos
            AgregarArticulo("Laptop", "Electrónica", 799.99m);
            AgregarArticulo("Smartphone", "Electrónica", 599.49m);
            AgregarArticulo("Cámara", "Fotografía", 450.75m);
            AgregarArticulo("Teclado", "Accesorios", 45.99m);
            AgregarArticulo("Mouse", "Accesorios", 29.99m);
            AgregarArticulo("Monitor", "Electrónica", 199.99m);
            AgregarArticulo("Impresora", "Oficina", 125.89m);
            AgregarArticulo("Audífonos", "Electrónica", 89.99m);
            AgregarArticulo("Tablet", "Electrónica", 349.99m);
            AgregarArticulo("Bocinas", "Audio", 119.99m);
            AgregarArticulo("Silla", "Muebles", 159.99m);
            AgregarArticulo("Escritorio", "Muebles", 249.99m);
            AgregarArticulo("Lámpara", "Iluminación", 39.99m);
            AgregarArticulo("Sofá", "Muebles", 899.99m);
            AgregarArticulo("Mesa", "Muebles", 199.99m);
            AgregarArticulo("Estante", "Muebles", 79.99m);
            AgregarArticulo("Reloj", "Decoración", 29.99m);
            AgregarArticulo("Cuadro", "Decoración", 49.99m);
            AgregarArticulo("Cortinas", "Decoración", 25.99m);
            AgregarArticulo("Alfombra", "Decoración", 99.99m);
            AgregarArticulo("Refri", "Electrodomésticos", 999.99m);
            AgregarArticulo("Estufa", "Electrodomésticos", 549.99m);
            AgregarArticulo("Lavadora", "Electrodomésticos", 749.99m);
            AgregarArticulo("Microondas", "Electrodomésticos", 119.99m);
            AgregarArticulo("Aspiradora", "Electrodomésticos", 199.99m);
            AgregarArticulo("Licuadora", "Cocina", 49.99m);
            AgregarArticulo("Cafetera", "Cocina", 79.99m);
            AgregarArticulo("Tostadora", "Cocina", 29.99m);
            AgregarArticulo("Olla", "Cocina", 59.99m);
            AgregarArticulo("Sarten", "Cocina", 39.99m);
            AgregarArticulo("Camisa", "Ropa", 24.99m);
            AgregarArticulo("Pantalones", "Ropa", 39.99m);
            AgregarArticulo("Zapatos", "Calzado", 79.99m);
            AgregarArticulo("Chaqueta", "Ropa", 99.99m);
            AgregarArticulo("Bufanda", "Accesorios", 14.99m);
            AgregarArticulo("Sombrero", "Accesorios", 19.99m);
            AgregarArticulo("Bolsa", "Accesorios", 49.99m);
            AgregarArticulo("Reloj de pulsera", "Accesorios", 129.99m);
            AgregarArticulo("Lentes de sol", "Accesorios", 59.99m);
            AgregarArticulo("Mochila", "Accesorios", 39.99m);
            AgregarArticulo("Colchón", "Dormitorio", 499.99m);
            AgregarArticulo("Sábanas", "Dormitorio", 59.99m);
            AgregarArticulo("Almohada", "Dormitorio", 29.99m);
            AgregarArticulo("Cobertor", "Dormitorio", 79.99m);
            AgregarArticulo("Cama", "Dormitorio", 699.99m);
            AgregarArticulo("Cómoda", "Dormitorio", 299.99m);
            AgregarArticulo("Ventilador", "Electrodomésticos", 79.99m);
            AgregarArticulo("Plancha", "Electrodomésticos", 49.99m);
            AgregarArticulo("Televisor", "Electrónica", 549.99m);
            AgregarArticulo("Reproductor Blu-ray", "Electrónica", 129.99m);

            // Casos de error

            //AgregarArticulo("M1crof0n0", "Música", 149.99m); Nombre cuenta con caracter/es invalido/s
            //AgregarArticulo("Micrófono", "Mús1c4", 149.99m); Categoria cuenta con caracter/es invalido/s
            //AgregarArticulo("Plancha", "Electrodomésticos", 49.99m); El articulo ingresado ya existe, reingrese uno nuevo.
            //AgregarArticulo("Bajo acustico", "Música", -10m); El monto del precio no puede ser negativo
            //AgregarArticulo("A", "Electrónica", 129.99m); El tamaño del nombre debe ser mayor o igual a 2, Ej: Té
            //AgregarArticulo("Reproductor Blu-ray", "Ele", 129.99m); El tamaño de la categoria debe ser mayor o igual a 4, Ej: Ropa
            //AgregarArticulo("", "Electrónica", 549.99m); El nombre es null o esta vacio
            //AgregarArticulo("Sábanas", "", 59.99m); La categoria es null o esta vacia



        }
        private void PrecargarUsuario()
        {
            // Casos de exito
            AgregarCliente(new Cliente("Carlos", "Martínez", "carlos.martinez@gmail.com", "Contraseña123"));
            AgregarCliente(new Cliente("Ana", "López", "ana.lopez@yahoo.com", "Segura2023"));
            AgregarCliente(new Cliente("Luis", "Fernández", "luis.fernandez@hotmail.com", "ClaveSegura#1"));
            AgregarCliente(new Cliente("Marta", "González", "marta.gonzalez@outlook.com", "Passw0rd!"));
            AgregarCliente(new Cliente("Javier", "Pérez", "javier.perez@correo.com", "ContraseñaFuerte@2024"));
            AgregarCliente(new Cliente("Laura", "Sánchez", "laura.sanchez@gmail.com", "Acceso*456"));
            AgregarCliente(new Cliente("David", "Hernández", "david.hernandez@yahoo.com", "MiClave789"));
            AgregarCliente(new Cliente("Sofía", "Ruiz", "sofia.ruiz@hotmail.com", "Sofia123$"));
            AgregarCliente(new Cliente("Alberto", "Ramírez", "alberto.ramirez@outlook.com", "Ramirez2024!"));
            AgregarCliente(new Cliente("Paula", "Castro", "paula.castro@correo.com", "PaulaSegura#2024"));

            AgregarAdministrador(new Administrador("Alberto", "Gomez", "alberto.gomez@outlook.com", "Ramirez2024!"));
            AgregarAdministrador(new Administrador("Paula", "Perez", "paula.perez@correo.com", "PaulaSegura#2024"));

            // Casos de error
            //AgregarUsuario("Paula", "Castro", "paula.castro@correo.com", "PaulaSegura#2024"); El usuario ingresado ya esta registrado
            //AgregarUsuario("Alberto", "Ramírez", "alberto.ramirezoutlook.com", "Ramirez2024!"); El formato del email debe ser email @example
            //AgregarUsuario("David", "Hernández", "david.hernandez@yahoo.com", ""); La contraseña no puede estar vacia
            //AgregarUsuario("", "Sánchez", "laura.sanchez@gmail.com", "Acceso*456"); El Nombre esta vacio
            //AgregarUsuario("Luis", "", "luis.fernandez@hotmail.com", "ClaveSegura#1"); El Apellido esta vacio

        }
    }
}
