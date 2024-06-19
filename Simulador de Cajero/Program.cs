using System;
using Simulador_de_Cajero.Models;

namespace CajeroAutomatico
{
    class Program
    {
        static Banco banco = new Banco();

        static void Main(string[] args)
        {
            InicializarDatos();
            while (true)
            {
                MostrarMenuPrincipal();
            }
        }

        static void InicializarDatos()
        {
            // Crear algunos usuarios de prueba
            banco.RegistrarUsuario(new Usuario("Admin Admin", "1234", "555-1234", "admin@example.com", "admin", "admin", new DateTime(1980, 1, 1), true));
            banco.RegistrarUsuario(new Usuario("Cliente Cliente", "5678", "555-5678", "cliente@example.com", "cliente", "cliente", new DateTime(1990, 2, 2), false));
        }

        static void MostrarMenuPrincipal()
        {
            Console.WriteLine("Bienvenido al Cajero Automático del Banco T1172");
            Console.Write("Ingrese su usuario: ");
            string usuario = Console.ReadLine();
            Console.Write("Ingrese su contraseña: ");
            string password = Console.ReadLine();

            Usuario usuarioActual = banco.IniciarSesion(usuario, password);
            if (usuarioActual != null)
            {
                if (usuarioActual.EsAdministrador)
                {
                    MostrarMenuAdministrador(usuarioActual);
                }
                else
                {
                    MostrarMenuCliente(usuarioActual);
                }
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas.");
            }
        }

        static void MostrarMenuAdministrador(Usuario usuarioActual)
        {
            Console.WriteLine($"Hola {usuarioActual.Nickname}, Bienvenido al Cajero Automático del Banco T1172");
            while (true)
            {
                Console.WriteLine("1. Registrar usuario");
                Console.WriteLine("2. Dar de baja un usuario");
                Console.WriteLine("3. Crear cuenta");
                Console.WriteLine("4. Dar de baja una cuenta");
                Console.WriteLine("5. Salir");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarUsuario();
                        break;
                    case "2":
                        DarDeBajaUsuario();
                        break;
                    case "3":
                        CrearCuenta();
                        break;
                    case "4":
                        DarDeBajaCuenta();
                        break;
                    case "5":
                        return; // Volver al menú principal
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void MostrarMenuCliente(Usuario usuarioActual)
        {
            Console.WriteLine($"Hola {usuarioActual.Nickname}, Bienvenido al Cajero Automático del Banco T1172");
            while (true)
            {
                Console.WriteLine("1. Depositar");
                Console.WriteLine("2. Retirar");
                Console.WriteLine("3. Verificar saldo");
                Console.WriteLine("4. Salir");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        Depositar(usuarioActual);
                        break;
                    case "2":
                        Retirar(usuarioActual);
                        break;
                    case "3":
                        VerificarSaldo(usuarioActual);
                        break;
                    case "4":
                        return; // Volver al menú principal
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void RegistrarUsuario()
        {
            Console.Write("Nombre Completo: ");
            string nombreCompleto = Console.ReadLine();
            Console.Write("Identidad: ");
            string identidad = Console.ReadLine();
            Console.Write("Teléfono Móvil: ");
            string telefonoMovil = Console.ReadLine();
            Console.Write("Correo Electrónico: ");
            string correoElectronico = Console.ReadLine();
            Console.Write("Nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Fecha de Nacimiento (yyyy-MM-dd): ");
            DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine());
            Console.Write("Es Administrador (true/false): ");
            bool esAdministrador = bool.Parse(Console.ReadLine());

            banco.RegistrarUsuario(new Usuario(nombreCompleto, identidad, telefonoMovil, correoElectronico, nickname, password, fechaNacimiento, esAdministrador));
            Console.WriteLine("Usuario registrado exitosamente.");
        }

        static void DarDeBajaUsuario()
        {
            Console.Write("Nickname del usuario a dar de baja: ");
            string nickname = Console.ReadLine();
            if (banco.EliminarUsuario(nickname))
            {
                Console.WriteLine("Usuario eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }

        static void CrearCuenta()
        {
            Console.Write("Identidad del propietario: ");
            string idPropietario = Console.ReadLine();
            Console.Write("Balance inicial: ");
            decimal balance = decimal.Parse(Console.ReadLine());
            banco.AgregarCuenta(idPropietario, balance);
            Console.WriteLine("Cuenta creada exitosamente.");
        }

        static void DarDeBajaCuenta()
        {
            Console.Write("ID de la cuenta a dar de baja: ");
            int idCuenta = int.Parse(Console.ReadLine());
            if (banco.EliminarCuenta(idCuenta))
            {
                Console.WriteLine("Cuenta eliminada exitosamente.");
            }
            else
            {
                Console.WriteLine("Cuenta no encontrada.");
            }
        }

        static void Depositar(Usuario usuarioActual)
        {
            var cuentas = banco.ObtenerCuentasDeUsuario(usuarioActual.Identidad);
            Console.WriteLine("Seleccione la cuenta:");
            foreach (var cuenta in cuentas)
            {
                Console.WriteLine($"ID: {cuenta.IdCuenta}, Balance: {cuenta.Balance}");
            }
            int idCuenta = int.Parse(Console.ReadLine());
            Console.Write("Monto a depositar: ");
            decimal monto = decimal.Parse(Console.ReadLine());
            var cuentaSeleccionada = banco.ObtenerCuenta(idCuenta);
            cuentaSeleccionada.Depositar(monto);
            Console.WriteLine("Depósito realizado exitosamente.");
        }

        static void Retirar(Usuario usuarioActual)
        {
            var cuentas = banco.ObtenerCuentasDeUsuario(usuarioActual.Identidad);
            Console.WriteLine("Seleccione la cuenta:");
            foreach (var cuenta in cuentas)
            {
                Console.WriteLine($"ID: {cuenta.IdCuenta}, Balance: {cuenta.Balance}");
            }
            int idCuenta = int.Parse(Console.ReadLine());
            Console.Write("Monto a retirar: ");
            decimal monto = decimal.Parse(Console.ReadLine());
            var cuentaSeleccionada = banco.ObtenerCuenta(idCuenta);
            if (cuentaSeleccionada.Retirar(monto))
            {
                Console.WriteLine("Retiro realizado exitosamente.");
            }
            else
            {
                Console.WriteLine("Saldo insuficiente.");
            }
        }

        static void VerificarSaldo(Usuario usuarioActual)
        {
            var cuentas = banco.ObtenerCuentasDeUsuario(usuarioActual.Identidad);
            Console.WriteLine("Saldo de sus cuentas:");
            foreach (var cuenta in cuentas)
            {
                Console.WriteLine($"ID: {cuenta.IdCuenta}, Balance: {cuenta.Balance}");
            }
        }
    }
}
