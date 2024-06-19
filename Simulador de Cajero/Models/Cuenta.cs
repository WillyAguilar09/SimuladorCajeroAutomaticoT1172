using System;

namespace Simulador_de_Cajero.Models
{
    public class Cuenta
    {
        public int IdCuenta { get; set; }
        public string IdPropietario { get; set; }
        public decimal Balance { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Cuenta(int idCuenta, string idPropietario, decimal balance)
        {
            IdCuenta = idCuenta;
            IdPropietario = idPropietario;
            Balance = balance;
            FechaCreacion = DateTime.Now;
        }

        public void Depositar(decimal monto)
        {
            Balance += monto;
        }

        public bool Retirar(decimal monto)
        {
            if (Balance >= monto)
            {
                Balance -= monto;
                return true;
            }
            return false;
        }
    }
}

