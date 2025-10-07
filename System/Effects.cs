using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public class Effects
    {
        public static void TypeLine(string text, int delay = 10)
        {
            foreach ( char c in text )
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void Type(string text, int delay = 10)
        {
            foreach ( char c in text )
            {
                Console.WriteLine(c);
                Thread.Sleep(delay);
            }
        }
        
        public static void PrintColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
