using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BibliotekaKlas
{
    class UserStorage
    {
        public static List<User> GetUsers(string filePath)
        {
            List<User> users = new List<User>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] lineParsed = line.Split(',');
                string login = lineParsed[0].Split(':')[1];
                string password = lineParsed[1].Split(':')[1];

                users.Add(new User(login, password));
            }

            return users;
        }

        public static bool AddUser(User user, string filePath)
        {
            List<User> users = GetUsers(filePath);

            bool userAlreadyExists = users.Exists(u => u.Login == user.Login);

            if (userAlreadyExists)
            {
                return false;
            }

            users.Add(user);

            List<string> usersStringList = new List<string>();

            foreach (User u in users)
            {
                usersStringList.Add(u.MapToString());
            }

            File.WriteAllLines(filePath, usersStringList);

            return true;
        }

        public static void ChangeUserPass(User user, string password, string filePath)
        {
            List<User> users = GetUsers(filePath);
            List<string> usersStringList = new List<string>();

            foreach (User u in users)
            {
                if (u.Login == user.Login)
                    u.Password = password;
                usersStringList.Add(u.MapToString());
            }

            File.WriteAllLines(filePath, usersStringList);
        }

    }
}
