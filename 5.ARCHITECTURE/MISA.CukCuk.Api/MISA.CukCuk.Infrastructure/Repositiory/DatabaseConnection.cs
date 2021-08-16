using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositiory
{
    class DatabaseConnection
    {
        public static IDbConnection DbConnection
        {
            get { return new MySqlConnection("Host=47.241.69.179;Database= MISA.CukCuk_Demo_NVMANH; User Id=dev; Password=12345678"); }
        }
    }
}
