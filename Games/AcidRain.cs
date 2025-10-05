using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    
    public class AcidRain : IGame
    {
        public string Id => "acid_rain";
        public string Title => "산성비";

        private static List<string> Word = new List<string>
        {
            "할당", "해제", "메모리", "가비지", "인터페이스", "stack", "heap", "data", "code", "gc",
            "static", "dynamic", "runtime", "int", "long", "short", "byte",
            "float", "double", "decimal", "char", "string", "bool", "variable",
            "null", "if", "switch", "for", "while", "casting", "boxing", "unboxing",
            "overflow", "method", "parameter", "argument", "class", "object", "instance",
            "field", "member", "overloading", "shadowing", "interface", "abstract", "dictionary",
            "generic", "namespace", "delegate", "lambda"
        };

        class DropWord
        {
            public string Text;
            public int X;
            public int Y;
            public Stopwatch FallTimer = new Stopwatch(); // 낙하 타이머 생성

            public DropWord(string text, int x, int y)
            {
                Text = text;
                X = x;
                Y = y;
                FallTimer.Start();
            }
        }

        public void Run()
        {
            var rnd = new Random();

            
        }


    }
}
