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

        // 메뉴 복귀 전까지 유지되는 통계
        private int totalWin = 0;
        private int totalLose = 0;
        private int totalDraw = 0;

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
                ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue };

                // 플레이어 색상 추가
                Console.Write("플레이어: ");
                Console.ForegroundColor = colors[Player];
                Console.Write(names[Player]);
                Console.ResetColor();

                // CPU 색상 추가
                Console.Write(" vs CPU: ");
                Console.ForegroundColor = colors[CPU];
                Console.Write(names[CPU]);
                Console.ResetColor();
                                
                //Console.WriteLine($"플레이어: {names[Player]} vs CPU: {names[CPU]}");

                if ( Player == CPU )
                {
                    Console.WriteLine(" 무승부!");
                    draw++;
                }
                else if ((Player == 0 && CPU == 2) || (Player == 1 && CPU == 0) || (Player == 2 && CPU == 1))
                {
                    Console.WriteLine(" 승리!");
                    win++;
                }
                else
                {
                    Console.WriteLine(" 패배...");
                    lose++;
                }
            }

            Console.WriteLine("\n=====최종 결과=====");
            if ( win > lose )
            {
                Console.WriteLine("플레이어 승리!");
            }
            else if ( lose > win )
            {
                Console.WriteLine("CPU 승리!");
            }
            else
            {
                Console.WriteLine("무승부로 끝났습니다.");
            }

            //Console.WriteLine($"\n통계 : {win}승 / {lose}패 / {draw}무\n");
            Console.Write("\n통계 : ");
            Effects.PrintColor($"{win}승 ", ConsoleColor.Green);
            Effects.PrintColor($"{lose}패 ", ConsoleColor.Red);
            Effects.PrintColor($"{draw}무\n", ConsoleColor.Yellow);

            totalWin += win;
            totalLose += lose;
            totalDraw += draw;

            //Console.WriteLine($"\n현재까지 누적 통계 : {totalWin}승 / {totalLose}패 / {totalDraw}무\n");
            Console.Write("\n누적 통계 : ");
            Effects.PrintColor($"{totalWin}승 ", ConsoleColor.Green);
            Effects.PrintColor($"{totalLose}패 ", ConsoleColor.Red);
            Effects.PrintColor($"{totalDraw}무\n", ConsoleColor.Yellow);
        }
    }
}
