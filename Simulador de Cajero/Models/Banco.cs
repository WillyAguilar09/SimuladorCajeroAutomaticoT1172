using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulador_de_Cajero.Models
{
    public class Banco
    {
        private List<Usuario> usuarios;
        private List<Cuenta> cuentas;
        private int siguienteIdCuenta;

        public Banco()
        {
            usuarios = new List<Usuario>();
            cuentas = new List<Cuenta>();
            siguienteIdCuenta = 1;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public bool EliminarUsuario(string nickname)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Nickname == nickname);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
                return true;
            }
            return false;
        }

        public Usuario IniciarSesion(string nickname, string password)
        {
            return usuarios.FirstOrDefault(u => u.Nickname == nickname && u.Password == password);
        }

        public void AgregarCuenta(string idPropietario, decimal balance)
        {
            cuentas.Add(new Cuenta(siguienteIdCuenta++, idPropietario, balance));
        }

        public bool EliminarCuenta(int idCuenta)
        {
            var cuenta = cuentas.FirstOrDefault(c => c.IdCuenta == idCuenta);
            if (cuenta != null)
            {
                cuentas.Remove(cuenta);
                return true;
            }
            return false;
        }

        public Cuenta ObtenerCuenta(int idCuenta)
        {
            return cuentas.FirstOrDefault(c => c.IdCuenta == idCuenta);
        }

        public IEnumerable<Cuenta> ObtenerCuentasDeUsuario(string idPropietario)
        {
            return cuentas.Where(c => c.IdPropietario == idPropietario);
        }
    }
}

