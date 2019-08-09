using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.DB
{
    public class DbNetshoesConnection
    {
        private SqlConnection connection = new SqlConnection();
        public DbNetshoesConnection()
        {
            connection.ConnectionString = @"Data Source=DESKTOP-E9GQC07\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
        }

        public SqlConnection Conectar()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            return connection;
        }

        public void Desconectar()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
