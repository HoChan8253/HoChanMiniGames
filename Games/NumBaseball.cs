using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public class NumBaseball : IGame
    {
        public string Id => "number_baseball";
        public string Title => "숫자 야구";

        int inning = 0;
        const int maxInning = 9; // 최대 10번 기회

        //랜덤한 4자리 숫자 생성
        private List<int> Answer(int length)
        {
            Random rnd = new Random();
            List<int> numbers = new List<int>(); // 비어있음

            while (numbers.Count < length) //length 즉 4개가 될 때까지 반복
            {
                int n = rnd.Next(0, 10); // 0 ~ 9 랜덤 숫자
                if (!numbers.Contains(n)) // 리스트 안에 값이 들어있는지 확인
                {
                    numbers.Add(n); // 리스트에 추가
                }
            }
            return numbers;
        }

        public void Run()
        {
            List<int> answer = Answer(4); // 랜덤 4자리 ( 중복 없어야함 )

            Console.WriteLine("숫자 야구 게임 시작! (서로 다른 4자리 숫자를 맞춰보세요.)");
            Console.WriteLine($"기회는 총 {maxInning}이닝 입니다.\n");
                        
            while (inning < maxInning)
            {
                inning++;
                string input = Input.ReadString($"{inning} 이닝 입력 (4자리 숫자) : ", false); 
                // 문자열로 받아와야함!! 숫자로 받아오면 4자리 숫자가 아니라 1천자리 숫자됨!!

                if (input.Length != 4 || !int.TryParse(input, out _))
                {
                    Console.WriteLine("4자리 숫자를 정확히 입력해주세요.");
                    inning--;
                    continue;
                }

                
                int strike = 0;
                int ball = 0;

                // Strike / Ball 판정
                for ( int i = 0; i < 4; i++ )
                {
                    int n = input[i];
                    if (answer[i] == n) // 자리와 숫자 모두 일치
                    {
                        strike++;
                    }
                    else if (answer.Contains(n)) // answer 컨테이너 안에 같은 값이 들어있는지 확인
                    {
                        ball++;
                    }
                }

                if ( strike == 4 )
                {
                    Console.WriteLine($"정답입니다! {inning}이닝 만에 맞췄습니다.");
                    return; // 게임 종료
                }
                else if ( strike == 0 && ball == 0 )
                {
                    //Console.WriteLine("OUT!");
                    Effects.PrintColor("OUT!\n", ConsoleColor.Red);
                }
                else
                {
                    //Console.WriteLine($"{strike} 스트라이크, {ball} 볼");
                    Effects.PrintColor($"{strike} 스트라이크", ConsoleColor.Green);
                    Effects.PrintColor($"{ball} 볼\n", ConsoleColor.Yellow);
                }
            }
            Console.WriteLine("\n 10이닝 안에 맞추지 못했습니다. 아쉽습니다.");

            string answerText = "";

            foreach ( int n in answer )
            {
                answerText = n.ToString(); // 정수를 문자열 변환
            }
            Console.WriteLine($"정답은 {answerText} 입니다.\n");
        }
    }
}
