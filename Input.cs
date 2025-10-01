using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    // 콘솔 입력과 관련된 공통 기능

    class Input
    {
        
        public static int ReadIntRange(string intro, int minN, int maxN)
        {
            while (true)
            {
                Console.Write(intro);
                string s = Console.ReadLine();

                // null 예외 처리
                if (s == null)
                {
                    Console.WriteLine("입력이 잘못 되었습니다.");
                    continue;
                }

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

        //▼ 예 / 아니오 로 확인 입력받기 y/yes , n/no 를 소문자로 통일하여 처리
        public static bool Confirm(string intro)
        {
            Console.WriteLine($"{intro} ( y/n ): ");
            while (true)
            {
                string s = Console.ReadLine();
                if (s != null)
                {
                    s = s.Trim().ToLower(); // 공백을 없애고 소문자로 변환
                }
                else
                {
                    s = ""; // null 일경우
                }

                if (s == "y" || s == "yes")
                {
                    return true;
                }

                if (s == "n" || s == "no")
                {
                    return false;
                }

                Console.WriteLine("Y 또는 N로 다시 입력해주세요.: ");
            }
        }

    }
}
