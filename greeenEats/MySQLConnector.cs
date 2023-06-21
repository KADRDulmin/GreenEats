using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace greeenEats
{
    class MySQLConnector
    {
        private MySqlConnection connection;
        private string server = "sql12.freemysqlhosting.net";
        private string database = "sql12627644";
        private string username = "sql12627644";
        private string password = "1Fy4wseAi7";

        public MySQLConnector(string serverName, string dbName, string dbUsername, string dbPassword)
        {
            server = serverName;
            database = dbName;
            username = dbUsername;
            password = dbPassword;
            string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public MySqlCommand CreateCommand(string query)
        {
            return new MySqlCommand(query, connection);
        }
    
}
}
