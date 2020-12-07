using System;
using System.Text.RegularExpressions;

namespace BibliotekaKlas
{
    public class TriangleType
    {
        private GroupCollection groups;
        public string type;
        public bool is_match;

        /// <summary>
        /// Konstruktor klasy. Przyjmuje parametr "data" typu string. Sprawdza, czy tekst sklada sie z 3 liczb calkowitych i wywoluje metode checkType().
        /// </summary>
        /// <param name="data"></param>
        public TriangleType(string data)
        {
            Regex reg = new Regex(@"^(\d+)\s(\d+)\s(\d+)$");
            Match match = reg.Match(data.Replace("\0", string.Empty));
            is_match = match.Success;
            groups = match.Groups;
            if (is_match)
                checkType();
        }

        /// <summary>
        /// Sprawdza, czy z podanych liczb mozna stworzyc trojkat i czy jest to trojkat ostrokatny, rozwartokatny czy prostokatny.
        /// </summary>
        public void checkType()
        {
            int a = Int32.Parse(groups[1].Value);
            int b = Int32.Parse(groups[2].Value);
            int c = Int32.Parse(groups[3].Value);

            if (a + b <= c || a + c <= b || b + c <= a)
            {
                is_match = false;
                return;
            }
            if (a * a + b * b == c * c || a * a + c * c == b * b || c * c + b * b == a * a)
                type = "Trojkat prostokatny\r\n";
            else if ((a * a + b * b < c * c && c > a && c > b) || (a * a + c * c < b * b && b > a && b > c) || (c * c + b * b < a * a && a > b && a > c))
                type = "Trojkat rozwartokatny\r\n";
            else
                type = "Trojkat ostrokatny\r\n";
        }

        /// <summary>
        /// Zwraca typ trojkata w zmiennej typu string.
        /// </summary>
        /// <returns></returns>
        public string GetTriangleType()
        {
            if (is_match)
                return type;
            else
                return "Podano bledne dane wejsciowe\r\n";
        }

        static void Main(string[] args)
        {
        }
    }
}
