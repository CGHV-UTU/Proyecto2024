using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackofficeDeAdministracion
{
    public class VerificarBaneosTemporales
    {
        private static Timer timer;
        static string connectionString = "server=localhost; database=base; uid=root;";

        public static void Iniciar()
        {
            timer = new Timer();
            timer.Interval = 18000;        
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {          
            VerificarBansTemporalesVencidos();
        }

        static void VerificarBansTemporalesVencidos()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {              
                MySqlCommand command = new MySqlCommand("SELECT NombreDeCuenta FROM base.usuarios WHERE estado_de_cuenta = 'temporal' AND fechaBaneoTemporal < NOW()", conn);
                try
                {
                    conn.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string NombreDeCuenta = reader.GetString("NombreDeCuenta");
                            Console.WriteLine($"Revertir ban temporal para el usuario: {NombreDeCuenta}");
                            ActualizarEstadoUsuario(NombreDeCuenta, "activo");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("No hay usuarios a desbanear");                   
                }
            }
        }

        static void ActualizarEstadoUsuario(string NombreDeCuenta, string nuevoEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand("UPDATE base.usuarios SET estado_de_cuenta = @NuevoEstado, fechaBaneoTemporal = NULL WHERE NombreDeCuenta = @NombreDeCuenta", conn);
                command.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                command.Parameters.AddWithValue("@NombreDeCuenta", NombreDeCuenta);
                try
                {
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Usuario '{NombreDeCuenta}' ha sido desbaneado.");
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró el usuario '{NombreDeCuenta}' en la base de datos.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar estado del usuario '{NombreDeCuenta}': " + ex.Message);
                }
            }
        }
    }
}
