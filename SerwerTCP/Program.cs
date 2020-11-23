using System;
using System.Net;
using BibliotekaKlas;

namespace SerwerTCP
{
    /// <summary>
    /// Program realizuje asynchroniczny serwer TCP.
    /// Serwer przyjmuje dane w postaci trzech liczb calkowitych oddzielonych spacja.
    /// Przyjete dane interpretuje jako dlugosci bokow trojkata i okresla jego rodzaj (ostrokatny, prostokatny, rozwartokatny).
    /// Serwer w odpowiedzi wysyla klientowi okreslony rodzaj. Wyswietla on rowniez przyjete dane oraz swoja odpowiedz w konsoli.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            ServerEchoAPM server = new ServerEchoAPM(IPAddress.Parse("127.0.0.1"), 8000);
            server.Start();
        }
    }
}
