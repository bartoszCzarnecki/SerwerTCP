using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Database
{
    class UserAuthentication
    {
        private readonly Database db;

        public UserAuthentication(){
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

        public bool MatchPassword(string id, string password)
        {
            string query = $"SELECT Count(*) FROM users WHERE id = UUID_TO_BIN('{id}') AND password = '{password}';";
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
