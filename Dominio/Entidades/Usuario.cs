using static System.Net.Mime.MediaTypeNames;

namespace Dominio.Entidades
{
    public abstract class Usuario : IEquatable<Usuario>
    {
        private static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        public Usuario()
        {
            Id = ++IdCount;
        }
        public Usuario(string nombre, string apellido, string email, string contrasenia)
        {
            Id = ++IdCount;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contrasenia = contrasenia;
        }
        public void Deconstruct(out string nombre, out string apellido)
        {
            nombre = Nombre;
            apellido = Apellido;
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
            bool number = false;
            bool word = false;
            int[] caracteresEspeciales = [32, 193, 201, 205, 209, 211, 218, 225, 233, 237, 241, 243, 250];
            if (string.IsNullOrEmpty(Contrasenia))
                throw new Exception("E-ContraseniaEmpty:La contraseña no puede estar vacia");
            if (Contrasenia.Length < 8)
                throw new Exception("E-ContraseniaTamanio:La contraseña debe tener como minimo 8 caracteres.");
            foreach (char elem in Contrasenia)
            {
                if ((caracteresEspeciales.Contains((int)elem) || ((int)elem >= 38 && (int)elem <= 46) || ((int)elem >= 65 && (int)elem <= 90) || ((int)elem >= 97 && (int)elem <= 122)) && !word)
                    word = true;
                if (((int)elem <= 57 && (int)elem >= 48) && !number)
                    number = true;
            }
            if (!number || !word)
                throw new Exception("E-ContraseniaNoNumero:La contraseña debe ser alfanumerica");
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
            return $"{Nombre} {Apellido}";
        }
        public override bool Equals(object? obj)
        {
            Usuario usuario = obj as Usuario;
            return usuario != null && Email.ToLower() == usuario.Email.ToLower();
        }
        public bool Equals(Usuario other)
        {
            return other != null && Email.Equals(other.Email);
        }
    }
}
