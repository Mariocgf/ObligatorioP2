namespace Dominio.Entidades
{
    public class Cliente : Usuario
    {
        public decimal Billetera { get; set; }

        public Cliente() { }

        public Cliente(string nombre, string apellido, string email, string contrasenia) : base(nombre, apellido, email, contrasenia)
        {
            Billetera = 0;
        }

        public void Deconstruct(out string nombre, out string apellido, out string email, out decimal billetera)
        {
            base.Deconstruct(out nombre, out apellido, out email);
            billetera = Billetera;
        }
        public override void Depositar(decimal monto)
        {
            if (monto < 0)
            {
                throw new Exception("E-MontoNegativo:El monto a depositar no pude ser negativo");
            }
            Billetera += monto;
        }
        public override string ToString()
        {
            return $"" +
                $"{base.ToString()}\n" +
                $"Billetera: {Billetera}\n";
        }
    }
}
