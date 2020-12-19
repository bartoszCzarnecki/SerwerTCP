using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    /// <summary>
    /// Klasa implementująca obsługę każdego klienta po tym, jak połączy się on z serwerem. Przedstawia mu dostępne opcje 
    /// oraz analizuje przesłane przez niego dane (jeżeli nie wykonuje on obecnie innej konkretnej czynności).
    /// <para>Klasa stwierdza, jaką wiadomość powinna wysłać na podstawie zmiennej 'status', którą zmieniają różne metody.</para>
    /// 
    /// </summary>
    public class UI
    {
        private string status = "menu";
        private readonly string filePath = "../../../../users.txt";
        private User loggeduser = null;

        /// <summary>
        /// Ustalanie wiadomości, jaka powinna w danym momencie trafić do klienta. 
        /// W zależności od wartości zmiennej 'status' zwracany jest tekst z innej metody.
        /// </summary>
        /// <param name="clientmsg"> Wiadomość, jaką wysłał klient </param>
        /// <returns></returns>
        public string ServerMsg(string clientmsg)
        {
            if (clientmsg == "get_status")
                return status;
            else if (status == "menu")
                return GetStartMenu();
            else if (status == "choice")
                return Choice(clientmsg);
            else if (status == "login")
                return Login(clientmsg);
            else if (status == "register")
                return Register(clientmsg);
            else if (status == "logged")
                return LoggedChoice(clientmsg);
            else if (status == "changepassword")
                return ChangePassword(clientmsg);
            else if (status == "triangle")
                return Triangle(clientmsg);
            else
                return GetStartMenu();
        }

        // Informacja o początkowych opcjach, jakie ma klient.
        private string GetStartMenu()
        {
            status = "choice";
            return "ok";
        }

        private string GetLoggedMenu()
        {
            status = "logged";
            return "ok";
        }

        // Przetworzenie wyboru z GetStartMenu()
        private string Choice(string msg)
        {
            int choice;
            if (Int32.TryParse(msg, out choice))
            {
            switch(choice)
                {
                    case 1:
                        status = "login";
                        return "ok";
                    case 2:
                        status = "register";
                        return "ok";
                    default:
                        status = "menu";
                        return "value_error";
                }
            }
            else
            {
                status = "choice";
                return "value_error";
            }
        }

        private string LoggedChoice(string msg)
        {
            int choice;
            if (Int32.TryParse(msg, out choice))
            {
                switch (choice)
                {
                    case 1:     // zmiana hasla
                        status = "changepassword";
                        return "ok";
                    case 2:     // liczenie bokow trojkata
                        status = "triangle";
                        return "ok";
                    case 3:     // usuwanie konta
                        UserStorage.DeleteUser(loggeduser, filePath);
                        loggeduser = null;
                        return GetStartMenu();
                    case 4:     // wylogowanie
                        loggeduser = null;
                        return GetStartMenu();
                    default:
                        return "value_error";
                }
            }
            else
            {
                return "value_error";
            }
        }

        private string Login(string msg)
        {
            if (msg == "cancel")
                return GetStartMenu();
            string[] credentials = msg.Split(' ');
            if (credentials.Length != 2)
                return "arg_count_error";
            if (UserAuthentication.MatchPassword(credentials[0], credentials[1], filePath, out loggeduser))
            {
                status = "logged";
                return GetLoggedMenu();
            }
            else
                return "value_error";
        }

        private string Register(string msg)
        {
            if (msg == "cancel")
                return GetStartMenu();
            string[] credentials = msg.Split(' ');
            if (credentials.Length != 2)
                return "arg_count_error";
            if (UserStorage.AddUser(new User(credentials[0], credentials[1]), filePath))
            {
                loggeduser = new User(credentials[0], credentials[1]);
                return GetLoggedMenu();
            }
            else
                return "Podany login jest zajety\r\n";
        }

        private string ChangePassword(string msg)
        {
            if (msg == "cancel")
                return GetLoggedMenu();
            string[] passwords = msg.Split(' ');
            if (passwords.Length != 2)
                return "arg_count_error";
            if (UserAuthentication.MatchPassword(loggeduser.Login, passwords[0], filePath, out loggeduser))
            {
                UserStorage.ChangeUserPass(loggeduser, passwords[1], filePath);
                status = "logged";
                return GetLoggedMenu();
            }
            else
                return "value_error";
        }
        private string Triangle(string msg)
        {
            if (msg == "cancel")
                return GetLoggedMenu();
            return new TriangleType(msg).GetTriangleType();
        }
    }
}
