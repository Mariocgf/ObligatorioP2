namespace Dominio.Entidades
{
    public abstract class Usuario
    {
        private static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        public Usuario(string nombre, string apellido, string email, string contrasenia)
        {
            Id = ++IdCount;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contrasenia = contrasenia;
        }

        public virtual void ValidarEmailFormato()
        {
            bool arroba = false;
            foreach (char elem in Email)
            {
                if (elem.Equals('@'))
                {
                    arroba = true;
                }
            }
            if (!arroba)
            {
                throw new Exception("E-EmailFormato:El formato del email debe ser email@example");
            }
        }
        public virtual void ValidarContrasenia()
        {
            if (string.IsNullOrEmpty(Contrasenia))
            {
                throw new Exception("E-ContraseniaEmpty:La contraseña no puede estar vacia");
            }
        }
        public virtual void ValidarCampo(string data, string tipo)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new Exception($"E-{tipo}Empty:El {tipo} esta vacio.");
            }
        }
        public virtual void Validar()
        {
            ValidarEmailFormato();
            ValidarContrasenia();
            ValidarCampo(Nombre, "Nombre");
            ValidarCampo(Apellido, "Apellido");
        }
        public abstract void Depositar(decimal monto);
        public override string ToString()
        {
            return $"Usuario: {Nombre} {Apellido}\nEmail: {Email}";
        }
        public override bool Equals(object? obj)
        {
            Usuario usuario = obj as Usuario;
            return usuario != null && Email.ToLower() == usuario.Email.ToLower();
        }
    }
}
