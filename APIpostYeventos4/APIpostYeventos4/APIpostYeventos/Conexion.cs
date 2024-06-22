using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIpostYeventos
{
    public class Conexion
    {
        public static MySqlConnection conexion()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost; database=base; uID=root;pwd=;");
                return connection;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
