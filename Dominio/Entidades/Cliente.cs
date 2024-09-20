namespace Dominio.Entidades
{
    public class Cliente : Usuario
    {
        public decimal Billetera { get; set; }

        public Cliente(string nombre, string apellido, string email, string contrasenia) : base (nombre, apellido, email, contrasenia)
        {
            Billetera = 0;
        }
    }
}
