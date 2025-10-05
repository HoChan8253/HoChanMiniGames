using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoChanMiniGames.Games
{
    // 프로그래밍 관련 퀴즈를 하나 제작
    // 여러 문제들 중 임의로 선정하여 하나 출제
    // 를 제작해보려고 하는데 일단은 하나만 제작하여 기능 테스트
    // 이후 리팩토링하며 업그레이드를 해보자
    public class Quiz : IGame
    {
        public string Id => "Quiz";
        public string Title => "퀴즈";

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("문제 : 지역 변수 , 매개 변수 , 함수 호출 정보 등에 저장되는 메모리 영역은?");
            Console.Write("답 : ");

            string input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            input = input.Trim();
            input = input.ToLowerInvariant(); // 대/소문자 차이 없이 비교하도록 소문자 변경

            if (input == "stack" || input == "스택")
            {
                Console.WriteLine("정답입니다.");
            }
            else
            {
                Console.WriteLine("오답입니다.");
            }

            Console.WriteLine("\n엔터를 눌러주세요.");
            Console.ReadLine();
        }
    }
}
