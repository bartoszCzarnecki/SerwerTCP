using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    class UserAuthentication
    {
        public static bool MatchPassword(string userLogin, string password, string filePath)
        {
            List<User> users = UserStorage.GetUsers(filePath);

            foreach (User user in users)
            {
                if (user.Login == userLogin)
                {
                    if (user.Password == password)
                    {
                        return true;

                    } else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
