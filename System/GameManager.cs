using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    // 전체 실행을 담당하는 클래스
    // 역할은 크게 3가지
    // 1. 게임 등록 관리 ( ID 가 중복되어선 안된다 )
    // 2. 메인 메뉴 표시 , 사용자 선택 처리 ( 미니게임 선택 , 종료 )
    // 3. 선택된 게임의 실행 루프 관리 ( 재도전 / 메인메뉴 복귀 )

    public class GameManager
    {
        private readonly List<IGame> games = new List<IGame>(); // 등록된 게임 목록

        private bool running = true; // 프로그램 메인메뉴 루프

        public void Register(IGame game) // 게임 등록
        {
            games.Add(game);

            /*foreach (IGame g in games)
            {
                if (g.Id == game.Id)
                {
                    return; // 등록된 게임은 패스
                }
            }*/
        }

        private void MainMenu() // 메인화면
        {
            Console.Clear();
            Console.WriteLine("==============================");
            Console.WriteLine("       HoChan MiniGames       ");
            Console.WriteLine("==============================");
            Console.WriteLine("[0] 종료");

            for ( int i = 0; i < games.Count; i++ )
            {
                Console.WriteLine($"[{i+1}] - {games[i].Title}");
            }
            Console.WriteLine();
        }
        
        
        public void Run() // 프로그램의 메인 루프를 담당
        {
            Console.Title = "HoChan Mini Games"; // 콘솔 창 제목 설정
            while ( running )
            {
                MainMenu();
                int choice = Input.ReadIntRange("선택: ", 0, games.Count); 

                if ( choice == 0 ) // 0 입력시 종료
                {
                    running = false; // running 루프 탈출
                    break;
                }

                var select = games[choice - 1]; 
                RunGameLoop(select);
            }
            Console.WriteLine("프로그램을 종료합니다.");
        }

        private void RunGameLoop(IGame game)
        {
            bool retry = true;

            while (retry)
            {
                Console.Clear();
                Console.WriteLine($"{game.Title} 시작");
                Console.WriteLine("-----");

                game.Run();

                Console.WriteLine("-----");
                Console.WriteLine($"{game.Title} 종료");
                Console.WriteLine();

                // 한 판이 끝날 때마다 사용자의 행동 묻기
                Console.WriteLine("[1] 재도전   [2] 메뉴로 돌아가기");
                int r = Input.ReadIntRange("선택: ", 1, 2);

                retry = (r == 1); // 1입력 받을 경우 재시도
            }
        }

    }
}
