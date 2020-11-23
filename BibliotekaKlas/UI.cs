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
        /// <summary>Implementacja poprzedniego sposobu działania serwera. Do usunięcia. </summary>
        public string Answer(string msg)
        {
            Console.Write(msg + "\t");
            TriangleType type = new TriangleType(msg);
            return type.getType();
        }

        public string GetMenu()
        {
            return "Ktora z ponizszych czynnosci chcesz wykonac?\n" +
                "1 - logowanie\n" +
                "2 - rejestracja\n";
        }
    }
}
