using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    class User
    {
        private string login;
        private string password;

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public string Login { get { return login; } set { login = value; } }

        public string Password { get { return password; } set { password = value; } }

        public string MapToString() => "login:" + Login + "," + "password:" + Password;
    }
}
