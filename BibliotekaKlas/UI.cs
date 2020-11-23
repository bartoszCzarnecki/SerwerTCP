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
        private readonly string menu = "Ktora z ponizszych czynnosci chcesz wykonac?\r\n" +
                "1 - zmiana hasla\r\n" +
                "2 - sprawdzenie rodzaju trojkata o bokach podanej dlugosci\r\n";
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
                        return "Podano zla wartosc. ";
                }
            }
            else
            {
                status = "choice";
                return "Podano zla wartosc";
            }
        }

        private string Login(string msg)
        {
            if (msg == "cancel")
                return GetStartMenu();
            string[] credentials = msg.Split(' ');
            if (credentials.Length != 2)
                return "Podano zla forme, nalezy wpisac dwa slowa oddzielone spacja (login i haslo)\r\n";
            if (UserAuthentication.MatchPassword(credentials[0], credentials[1], filePath))
            {
                status = "logged";
                return menu;
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
                status = "logged";
                return menu;
            }
            else
                return "Podany login jest zajety\r\n";
        }
    }
}
