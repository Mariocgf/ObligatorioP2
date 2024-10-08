namespace Dominio.Entidades
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre, string apellido, string email, string contrasenia) : base(nombre, apellido, email, contrasenia) 
        { 
        }

        public override void Depositar(decimal monto) {}
    }
}
