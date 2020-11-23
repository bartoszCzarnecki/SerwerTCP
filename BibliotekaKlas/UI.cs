using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    /// <summary>
    /// Klasa implementująca obsługę każdego klienta po tym, jak połączy się on z serwerem. Przedstawia mu dostępne opcje 
    /// oraz analizuje przesłane przez niego dane (jeżeli nie wykonuje on obecnie innej konkretnej czynności).
    /// </summary>
    public class UI
    {
        string instance = "menu";

        public string ServerMsg(string clientmsg)
        {
            if (instance == "menu")
                return GetMenu();
            else if (instance == "choice")
                return Choice(clientmsg);
            else
                return "Blad";
        }

        public string GetMenu()
        {
            instance = "choice";
            return "Ktora z ponizszych czynnosci chcesz wykonac?\r\n" +
                "1 - logowanie\r\n" +
                "2 - rejestracja\r\n";
        }

        public string Choice(string msg)
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
