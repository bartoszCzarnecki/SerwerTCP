using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotekaKlas
{
    public class UI
    {
        public byte[] temp(string msg)
        {
            Console.Write(msg + "\t");
            TriangleType type = new TriangleType(msg);
            byte[] answer = System.Text.Encoding.ASCII.GetBytes(type.getType());
            Console.Write(type.getType());
            return answer;
        }
    }
}
