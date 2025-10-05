using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    // 콘솔 입력과 관련된 공통 기능
    // 원하는 문자열이나 숫자 외에 다른 값이 들어왔을 때 예외처리

    public static class Input
    {
        private static string ReadLineSafe()
        {
            string s = Console.ReadLine();
            return s ?? string.Empty; // Null 병합 연산자 , s 가 null 이면 Empty 반환
        }

        private static string Normalize(string s)
        {
            if (s == null)
            {
                return string.Empty;
            }
            s = s.Trim().ToLower();
            return s;
        }

        public static int ReadIntRange(string prompt, int minN, int maxN)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = ReadLineSafe();

                int n = 0;
                
                // 예외 처리
                if (int.TryParse(s, out n) && n >= minN && n <= maxN)
                {
                    return n;
                }
                else
                {
                    Console.WriteLine($"{minN} ~ {maxN} 사이의 숫자로 다시 입력해주세요.");
                }
            }
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = ReadLineSafe();

                int n = 0;
                if (int.TryParse(s , out n))
                {
                    return n;
                }
                else
                {
                    Console.WriteLine("숫자로 다시 입력해주세요.");
                }
            }
        }

        //▼ 예 / 아니오 로 확인 입력받기 y/yes , n/no 를 소문자로 통일하여 처리
        public static bool Confirm(string prompt)
        {
            Console.WriteLine($"{prompt} ( y/n ): ");
            while (true)
            {
                string s = Normalize(ReadLineSafe());
                
                if (s == "y" || s == "yes")
                {
                    return true;
                }

                if (s == "n" || s == "no")
                {
                    return false;
                }

                Console.WriteLine("y 또는 n로 다시 입력해주세요.: ");
            }
        }

        public static string ReadString(string prompt, bool isSpace)
        {
            while(true)
            {
                Console.Write(prompt);

                string s = ReadLineSafe();

                if (!isSpace && s.Trim().Length == 0)
                {
                    Console.WriteLine("빈 입력은 허용하지 않습니다. 다시 입력해주세요.");
                    continue;
                }
                return s.Trim();
            }
        }

        public static string NormalizeInput(string s)
        {
            string x = s;
            x = x ?? "";
            x = x.Trim();
            x = x.ToLowerInvariant(); // 대소문자 구분 제거
            return x;
        }
    }
}
