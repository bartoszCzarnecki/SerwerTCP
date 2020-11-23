using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    /// <summary>
    /// Klasa implementująca obsługę każdego klienta po tym, jak połączy się on z serwerem. Przedstawia mu dostępne opcje 
    /// oraz analizuje przesłane przez niego dane (jeżeli nie wykonuje on obecnie innej konkretnej czynności).
    /// <para>Klasa stwierdza, jaką wiadomość powinna wysłać na podstawie zmiennej 'instance', którą zmieniają różne metody.</para>
    /// 
    /// </summary>
    public class UI
    {
        private string instance = "menu";
        
        /// <summary>
        /// Ustalanie wiadomości, jaka powinna w danym momencie trafić do klienta. 
        /// W zależności od wartości zmiennej 'instance' zwracany jest tekst z innej metody.
        /// </summary>
        /// <param name="clientmsg"> Wiadomość, jaką wysłał klient </param>
        /// <returns></returns>
        public string ServerMsg(string clientmsg)
        {
            if (instance == "menu")
                return GetMenu();
            else if (instance == "choice")
                return Choice(clientmsg);
            else
                return "Blad";
        }

        // Informacja o początkowych opcjach, jakie ma klient.
        private string GetMenu()
        {
            instance = "choice";
            return "Ktora z ponizszych czynnosci chcesz wykonac?\r\n" +
                "1 - logowanie\r\n" +
                "2 - rejestracja\r\n";
        }

        // Przetworzenie wyboru z GetMenu()
        private string Choice(string msg)
        {
            int choice;
            if (Int32.TryParse(msg, out choice))
            {
            switch(choice)
                {
                    case 1:
                        instance = "login";
                        return "Podaj login i haslo: ";
                    case 2:
                        instance = "register";
                        return "Podaj login i haslo: ";
                    default:
                        instance = "menu";
                        return "Podano zla wartosc. ";
                }
            }
            else
            {
                instance = "menu";
                return "Podano zla wartosc";
            }
        }
    }
}
