using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.api.Controllers
{
    public class DatabaseConnection
    {
        
        public static IDbConnection dbConnection = new MySqlConnection("Host=47.241.69.179;Database= MISA.CukCuk_Demo_NVMANH; User Id=dev; Password=12345678");

        public static IDbConnection DbConnection
        {
            get { return dbConnection; }
        }
    }
}
