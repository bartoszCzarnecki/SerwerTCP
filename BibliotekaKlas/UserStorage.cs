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

        public static void AddUser(User user, string filePath)
        {
            List<User> users = UserStorage.GetUsers(filePath);
            users.Add(user);

            List<String> usersStringList = new List<string>();

            foreach (User u in users)
            {
                usersStringList.Add(u.MapToString());
            }

            File.WriteAllLines(filePath, usersStringList);
        }
    }
}
