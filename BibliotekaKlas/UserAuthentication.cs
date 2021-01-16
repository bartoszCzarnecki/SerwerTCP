using System;
using System.Data;
using System.Security.Cryptography;
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
            string query = $"SELECT password FROM users WHERE login = '{login}'", hashPassword;
            if (db.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                MySqlDataReader r = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (r.Read())
                {
                    hashPassword = r["password"].ToString();
                    r.Close();
                    db.CloseConnection();
                    return VerifyPassword(hashPassword, password);
                }
            }
            return false;
        }
        public static string HashPassword(string password)
        {
            byte[] salt, hash, hashBytes = new byte[36];
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            hash = new Rfc2898DeriveBytes(password, salt, 100000).GetBytes(20);

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
        public static bool VerifyPassword(string hashPassword, string password)
        {
            byte[] salt = new byte[16], hash, hashBytes = Convert.FromBase64String(hashPassword);

            Array.Copy(hashBytes, 0, salt, 0, 16);
            hash = new Rfc2898DeriveBytes(password, salt, 100000).GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i]) return false;

            return true;
        }

    }
}
