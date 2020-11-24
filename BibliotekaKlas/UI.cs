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
            if (status == "menu")
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
            return "Ktora z ponizszych czynnosci chcesz wykonac?\r\n" +
                "1 - logowanie\r\n" +
                "2 - rejestracja\r\n";
        }

        private string GetLoggedMenu()
        {
            status = "logged";
            return "Ktora z ponizszych czynnosci chcesz wykonac?\r\n" +
                "1 - zmiana hasla\r\n" +
                "2 - sprawdzenie rodzaju trojkata o bokach podanej dlugosci\r\n" +
                "3 - wylogowanie\r\n";
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
                        return "Podaj login i haslo (oddzielone spacja) lub 'cancel' aby wrocic do menu:\r\n";
                    case 2:
                        status = "register";
                        return "Podaj login i haslo (oddzielone spacja) lub 'cancel' aby wrocic do menu:\r\n";
                    default:
                        status = "menu";
                        return "Podano zla wartosc\r\n";
                }
            }
            else
            {
                status = "choice";
                return "Podano zla wartosc\r\n";
            }
        }

        private string LoggedChoice(string msg)
        {
            int choice;
            if (Int32.TryParse(msg, out choice))
            {
                switch (choice)
                {
                    case 1:
                        status = "changepassword";
                        return "Podaj swoje stare haslo oraz nowe haslo (oddzielone spacja) lub 'cancel' aby wrocic do menu:\r\n";
                    case 2:
                        status = "triangle";
                        return "Podaj wartosci bokow (oddzielone spacja) lub 'cancel' aby wrocic do menu:\r\n";
                    case 3:
                        loggeduser = null;
                        return GetStartMenu();
                    default:
                        status = "menu";
                        return "Podano zla wartosc\r\n";
                }
            }
            else
            {
                status = "choice";
                return "Podano zla wartosc\r\n";
            }
        }

        private string Login(string msg)
        {
            if (msg == "cancel")
                return GetStartMenu();
            string[] credentials = msg.Split(' ');
            if (credentials.Length != 2)
                return "Podano zla forme, nalezy wpisac dwa slowa oddzielone spacja (login i haslo)\r\n";
            if (UserAuthentication.MatchPassword(credentials[0], credentials[1], filePath, out loggeduser))
            {
                status = "logged";
                return GetLoggedMenu();
            }
            else
                return "Podano zly login lub haslo\r\n";
        }

        private string Register(string msg)
        {
            if (msg == "cancel")
                return GetStartMenu();
            string[] credentials = msg.Split(' ');
            if (credentials.Length != 2)
                return "Podano zla forme, nalezy wpisac dwa slowa oddzielone spacja (login i haslo)\r\n";
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
                return "Podano zla forme, nalezy wpisac dwa slowa oddzielone spacja (stare i nowe haslo)\r\n";
            if (UserAuthentication.MatchPassword(loggeduser.Login, passwords[0], filePath, out loggeduser))
            {
                UserStorage.ChangeUserPass(loggeduser, passwords[1], filePath);
                status = "logged";
                return GetLoggedMenu();
            }
            else
                return "Podano zly login lub haslo\r\n";
        }
        private string Triangle(string msg)
        {
            if (msg == "cancel")
                return GetLoggedMenu();
            status = "logged";
            return new TriangleType(msg).GetTriangleType();
        }
    }
}
