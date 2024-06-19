using System;


namespace Simulador_de_Cajero.Models
{
    public class Usuario
    {
        public string NombreCompleto { get; set; }
        public string Identidad { get; set; }
        public string TelefonoMovil { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaCreacionUsuario { get; set; }
        public bool EsAdministrador { get; set; }

        public Usuario(string nombreCompleto, string identidad, string telefonoMovil, string correoElectronico,
            string nickname, string password, DateTime fechaNacimiento, bool esAdministrador)
        {
            NombreCompleto = nombreCompleto;
            Identidad = identidad;
            TelefonoMovil = telefonoMovil;
            CorreoElectronico = correoElectronico;
            Nickname = nickname;
            Password = password;
            FechaNacimiento = fechaNacimiento;
            FechaCreacionUsuario = DateTime.Now;
            EsAdministrador = esAdministrador;
        }
    }
}

