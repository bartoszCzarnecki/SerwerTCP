using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database
{
    class Database
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;


        public Database()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "usersdb";
            user = "root";
            password = "YOUR_PASSWORD";
            string value = $"SERVER={server};DATABASE={database};UID={user};PASSWORD={password}";

            connection = new MySqlConnection(value);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //Exception codes
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
