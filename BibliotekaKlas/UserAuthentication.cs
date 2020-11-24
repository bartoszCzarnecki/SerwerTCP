using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    class UserAuthentication
    {
        public static bool MatchPassword(string userLogin, string password, string filePath, out User user)
        {
            List<User> users = UserStorage.GetUsers(filePath);

            foreach (User us in users)
            {
                if (us.Login == userLogin)
                {
                    if (us.Password == password)
                    {
                        user = us;
                        return true;

                    } else
                    {
                        user = null;
                        return false;
                    }
                }
            }

            user = null;
            return false;
        }
    }
}
