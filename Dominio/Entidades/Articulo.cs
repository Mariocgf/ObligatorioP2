namespace Dominio.Entidades
{
    public class Articulo
    {
        private static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }

        public Articulo(string nombre, string categoria, decimal precio)
        {
            Id = ++IdCount;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
        }

        public void ValidarCaracteres(string letra, string tipo)
        {
            // 32 38-46 65-90 97-122 espacio y caracteres (mayusculas y minusculas)
            // (225-233-237-243-250) = á, é, í, ó, ú
            // (193-201-205-211-218) =  Á, É, Í, Ó, Ú
            // (209-241) = ñ, Ñ

            int[] caracteresEspeciales = [32, 193, 201, 205, 209, 211, 218, 225, 233, 237, 241, 243, 250];
            int cont = 0;
            foreach (char c in letra)
            {
                if (caracteresEspeciales.Contains((int)c) || ((int)c >= 38 && (int)c <= 46) || ((int)c >= 65 && (int)c <= 90) || ((int)c >= 97 && (int)c <= 122) )
                {
                    cont++;
                }
            }
            if (cont != letra.Length)
            {
                throw new Exception($"E-Caracter{tipo}:{tipo} cuenta con caracter/es invalido/s");
            }
        }
        private void ValidarPrecio(decimal precio)
        {
            if (precio < 0)
            {
                throw new Exception("E-PrecioNeg:El monto del precio no puede ser negativo");
            }
        }

        public void Validar()
        {
            ValidarCaracteres(Nombre, "Nombre");
            ValidarCaracteres(Categoria, "Categoria");
            ValidarPrecio(Precio);
        }

        public override string ToString()
        {
            return $"" +
                $"ID: {Id}\n" +
                $"Nombre: {Nombre}\n" +
                $"Categoria: {Categoria}\n" +
                $"Precio: {Precio}\n";
        }

        public override bool Equals(object? obj)
        {
            Articulo articulo = obj as Articulo;
            return articulo != null && Nombre.ToUpper() == articulo.Nombre.ToUpper();
        }
    }
}
