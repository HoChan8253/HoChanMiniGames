using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //foreach (IGame g in games)
            //{
            //    if (g.Id == game.Id)
            //    {
            //        return; // 등록된 게임은 패스
            //    }
            //}
        }

        private void MainMenu()
        {
            
        }
        
        
        public void Run()
        {
            Console.Title = "HoChan Minigames"; // 콘솔 창 제목 설정
            while ( running )
            {
                MainMenu();
            }
        }


    }
}
