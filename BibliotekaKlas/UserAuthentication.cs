using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace BibliotekaKlas
{
    class UserAuthentication
    {
        private readonly Database db;

        public UserAuthentication()
        {
            db = new Database();
        }

        public bool CheckAvailableLogin(string login)
        {
            string query = $"SELECT Count(*) FROM users WHERE login = '{login}';";
            int value = 0;
            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                value = int.Parse(cmd.ExecuteScalar() + "");
                db.CloseConnection();
            }
            return value == 0;
        }

        public bool MatchPassword(string login, string password)
        {
            string query = $"SELECT Count(*) FROM users WHERE login = '{login}' AND password = '{password}';";
            int value = 0;
            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                value = int.Parse(cmd.ExecuteScalar() + "");
                db.CloseConnection();
            }
            return value > 0;
        }
    }
}
