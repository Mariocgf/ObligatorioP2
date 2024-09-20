﻿namespace Dominio.Entidades
{
    public class Usuario
    {
        private static int IdCount { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        public Usuario (string nombre, string apellido, string email, string contrasenia)
        {
            Id = ++IdCount;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contrasenia = contrasenia;
        }

        
        public override string ToString()
        {
            return $"Usuario: {Nombre} {Apellido}\nEmail: {Email}";
        }
    }
}
