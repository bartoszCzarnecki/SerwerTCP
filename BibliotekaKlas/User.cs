using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    class User
    {
        private string id;
        private string login;
        private string password;
        private string createdOn;

        public User(string id, string login, string password, string createdOn)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.createdOn = createdOn;
        }

        public string Id { get { return id; } set { id = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string CreatedOn { get { return createdOn; } set { createdOn = value; } }

        public string toString() => $"[USER] id: {id}, login: {login}, password: {password}, created on: {createdOn}";
    }
}
