using System.Data;
using MySql.Data.MySqlClient;

namespace BibliotekaKlas
{
    class UserOperations
    {
        private readonly Database db;

        public UserOperations()
        {
            db = new Database();
        }

        public void Add(string login, string password)
        {
            string hashPassword = UserAuthentication.HashPassword(password);
            string query = $"INSERT INTO Users (id, login, password, createdOn) " +
                $"VALUES(UUID_TO_BIN(UUID()),'{login}', '{hashPassword}',  NOW() + INTERVAL 1 HOUR)";

            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                cmd.ExecuteNonQuery();
                db.CloseConnection();
            }
        }
        public void Remove(string id)
        {
            string query = $"DELETE FROM Users WHERE id=UUID_TO_BIN('{id}')";

            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                cmd.ExecuteNonQuery();
                db.CloseConnection();
            }
        }
        public void ChangePassword(string id, string password)
        {
            string hashPassword = UserAuthentication.HashPassword(password);
            string query = $"UPDATE Users SET password='{hashPassword}' WHERE id=UUID_TO_BIN('{id}');";

            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand
                {
                    CommandText = query,
                    Connection = db.connection
                };
                cmd.ExecuteNonQuery();
                db.CloseConnection();
            }
        }

        public User Get(string login = "", string id = "")
        {
            string query = $"SELECT BIN_TO_UUID(id) as id, login, password, createdOn FROM users ";
            query += login == "" ? $"WHERE id = UUID_TO_BIN('{id}')" : $"WHERE login = '{login}'";
            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                MySqlDataReader r = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (r.Read())
                {
                    User user = new User(r["id"].ToString(), r["login"].ToString(), r["password"].ToString(), r["createdOn"].ToString());
                    r.Close();
                    db.CloseConnection();
                    return user;
                }
            }
            return null;
        }
    }
}
