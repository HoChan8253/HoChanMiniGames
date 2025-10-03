using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public class RPS : IGame
    {
        public string Id => "rps";
        public string Title => "가위 바위 보";

        public void Run()
        {
            int win = 0; 
            int lose = 0;
            int draw = 0; // 통계
            int round = 0; 

            Console.WriteLine("가위 바위 보 시작! ( 5판 3선승제 )");

            while ( win < 3 && lose < 3 && round < 5 )
            {
                round++;
                Console.WriteLine($"\n[{round} 라운드 : 가위(0), 바위(1), 보(2) 중 선택하세요.");

                int Player = Input.ReadIntRange("입력: ", 0, 2); // Input 에서 0 ~ 2 안전 입력
                int CPU = new Random().Next(0, 3);

                string[] names = { "가위", "바위", "보" }; // 가위 0 , 바위 1 , 보 2
                Console.WriteLine($"플레이어: {names[Player]} vs CPU: {names[CPU]}");

                if ( Player == CPU )
                {
                    Console.WriteLine("무승부!");
                    draw++;
                }
                else if ((Player == 0 && CPU == 2) || (Player == 1 && CPU == 0) || (Player == 2 && CPU == 1))
                {
                    Console.WriteLine("승리!");
                    win++;
                }
                else
                {
                    Console.WriteLine("패배...");
                    lose++;
                }
            }

            Console.WriteLine("\n=====최종 결과=====");
            if ( win > lose )
            {
                Console.WriteLine("플레이어 승라!");
            }
            else if ( lose > win )
            {
                Console.WriteLine("CPU 승리!");
            }
            else
            {
                Console.WriteLine("무승부로 끝났습니다.");
            }

            Console.WriteLine($"\n통계 : {win}승 / {lose}패 / {draw}무\n");
        }
    }
}
