using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BibliotekaKlas
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
            password = "SqLexample123";
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
                        throw new InvalidOperationException("Cannot connect to server.");
                    case 1045:
                        throw new InvalidOperationException("Invalid username/password, please try again");
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
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
