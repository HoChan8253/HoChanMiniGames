using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public struct Pos // 좌표 값을 담는 구조체
    {
        public int X;
        public int Y;

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public enum Direction
        {
            Up, Down, Left, Right
        }
    }

    public class Snake : IGame
    {
        public string Id => "snake_game";
        public string Title => "스네이크 게임";

        #region Board Settings
        // 보드 설정
        private const int BoardWidth = 30; // 가로 X
        private const int BoardHeight = 30; // 세로 Y

        private const int TickMs = 120; // 게임 속도

        // 2차원 배열 보드
        private static char[,] board;

        // 2차원 배열에 그릴 문자들
        private const char Wall = '#';
        private const char Empty = ' ';
        private const char Food = '*';
        private const char Head = 'O';
        private const char Body = 'o';

        // 뱀 몸체 : 머리는 Last , 꼬리는 First
        private static LinkedList<Pos> snake;

        // 이동 방향
        private static Pos.Direction dir;

        // 먹이 좌표
        private static Pos food;
        #endregion

        // 점수 ( 먹이 하나당 100점 )
        private static int score = 0;

        // 게임 속도
        private const int gameSpeed = 120;

        private static bool isGameOver = false;

        private static Random rnd = new Random();

        public void Run()
        {
            bool hideCursor = Console.CursorVisible;
            Console.CursorVisible = false;

            Console.Clear();

            GameSetting();
            GameLoop();
            GameOver();

        }

        private static void GameSetting()
        {
            score = 0;
            isGameOver = false;
            // 2차원 보드 생성
            board = new char[BoardHeight, BoardWidth];

            // 전체를 빈 칸으로 채우기
            int x = 0;
            while (x < BoardHeight)
            {
                int y = 0;
                while (y < BoardWidth)
                {
                    board[x, y] = Empty; // 현재 위치에 채우기
                    y = y + 1; // y 좌표 하나씩 이동하면서 채우기
                }
                x = x + 1; // x 좌표 하나씩 이동하면서 채우기
            }

            // 외곽에 벽 세우기
            int wallX = 0;

            while (wallX < BoardWidth) // 위와 아래 벽 세우기
            {
                board[0, wallX] = Wall;
                board[BoardHeight - 1, wallX] = Wall;
                wallX = wallX + 1; // 한 칸씩 이동하면서 채우기
            }

            int wallY = 0;
            while (wallY < BoardHeight) // 좌측과 우측 벽 세우기
            {
                board[wallY, 0] = Wall;
                board[wallY, BoardWidth - 1] = Wall;
                wallY = wallY + 1;
            }

            // 뱀 초기 위치 ( 중앙 )
            int startX = BoardWidth / 2;
            int startY = BoardHeight / 2;

            snake = new LinkedList<Pos>();

            Pos p1 = new Pos(startX - 2, startY);
            Pos p2 = new Pos(startX - 1, startY);
            Pos p3 = new Pos(startX, startY);

            snake.AddLast(p1);
            snake.AddLast(p2);
            snake.AddLast(p3);

            dir = Pos.Direction.Right; // 초기 방향

            // 먹이 생성 ( 보드 / 뱀과 겹치면 안됨! )
            SpawnFood();

            // 현재 상태를 보드에 반영하고 그리기
            RefreshBoard();
            Update();
            UpdateHUD();
        }

        private static void GameLoop()
        {
            while (!isGameOver)
            {
                HandleInput();
                Step();
                RefreshBoard();
                Update();
                UpdateHUD();

                Thread.Sleep(gameSpeed);
            }
        }

        // 게임오버 안내
        private static void GameOver()
        {
            Console.SetCursorPosition(0, BoardHeight + 2);
            Console.WriteLine("======GAME OVER======");
            Console.Write("Result : ");
            Console.WriteLine("score");
            Console.WriteLine("아무 키나 눌러주세요.");
            Console.ReadKey(true);
        }

        private static bool IsOnSnake(Pos p) //링크드리스트 순회하며 특정 좌표가 뱀 몸 위치인지 확인
        {
            LinkedListNode<Pos> node = snake.First;
            while (node != null)
            {
                Pos s = node.Value;
                if (s.X == p.X && s.Y == p.Y)
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }
        
        private static void SpawnFood()
        {
            // Board 내부 ( 벽을 제외 ) 난수 좌표 뽑아
            // 뱀 몸과 겹치지 않게 생성해야함!!

            int trySpawn = 0;
            int tryMaxSpawn = 1000;

            while ( trySpawn < tryMaxSpawn )
            {
                int foodX = rnd.Next(1, BoardWidth - 1);
                int foodY = rnd.Next(1, BoardHeight - 1);

                Pos spawnPos = new Pos(foodX, foodY);

                // 뱀 위인지 확인 : 링크드리스트로 순회하며 체크
                if ( !IsOnSnake(spawnPos))
                {
                    food = spawnPos;
                    return;
                }
                trySpawn++;
            }
        }

        private static void HandleInput() // 키입력 처리 ( 뱀 머리 반대방향 즉시 전환 금지 )
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.Key == ConsoleKey.UpArrow)
                {
                    if(dir != Pos.Direction.Down)
                    {
                        dir = Pos.Direction.Up;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (dir != Pos.Direction.Up)
                    {
                        dir = Pos.Direction.Down;
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (dir != Pos.Direction.Right)
                    {
                        dir = Pos.Direction.Left;
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (dir != Pos.Direction.Left)
                    {
                        dir = Pos.Direction.Right;
                    }
                }
            }
        }

        private static void Step() // 이동 / 충돌 / 성장
        {
            // 현재 머리 좌표 ( Last 로 머리 사용 )
            Pos head = snake.Last.Value;

            int nextX = head.X;
            int nextY = head.Y;

            if (dir == Pos.Direction.Up)
            {
                nextY = nextY - 1;
            }
            else if (dir == Pos.Direction.Down)
            {
                nextY = nextY + 1;
            }
            else if (dir == Pos.Direction.Left)
            {
                nextX = nextX - 1;
            }
            else if(dir == Pos.Direction.Right)
            {
                nextX = nextX + 1;
            }

            Pos nextHead = new Pos(nextX, nextY);

            // 먹이를 먹는지 판단
            bool willEat = (nextHead.X == food.X) && (nextHead.Y == food.Y);

            // 충돌 판단을 위해 "다음 칸"의 현재 보드 문자 확인
            // 다음 칸이 꼬리이고 이번 턴에 성장 ( 먹이를 먹지 않으면 )하지 않으면,
            // 꼬리는 곧 빠질 예정이므로 충돌이 아니다!!
            char nextCell = board[nextY, nextX];

            // 꼬리 좌표
            Pos tail = snake.First.Value;

            // 다음 칸이 현재 꼬리 칸인지 판정
            bool isNextTail = (nextHead.X == tail.X) && (nextHead.Y == tail.Y);

            // 벽 충돌 확인
            if (nextCell == Wall)
            {
                isGameOver = true;
                return;
            }

            // 몸 충돌
            if (nextCell == Body)
            {
                bool conflictBody = true;

                if (isNextTail && !willEat) // 꼬리 위치이고 성장하지 않는다면
                {
                    conflictBody = false; // 충돌이 아님
                }

                if (conflictBody)
                {
                    isGameOver = true;
                    return;
                }
            }

            // 이동 처리
            snake.AddLast(nextHead);

            // 먹이를 먹었을때 점수 계산
            if (willEat)
            {
                score += 100;

                SpawnFood();
            }
            else
            {
                snake.RemoveFirst(); //먹이를 먹지 않으면 전진 + 꼬리 제거
            }
        }

        // 현재 상태 ( 벽 / 빈칸 / 먹이 / 뱀 )를 보드에 반영해야함
        private static void RefreshBoard()
        {
            // 보드 전체를 빈칸으로 리셋
            int x = 0;
            while (x < BoardHeight)
            {
                int y = 0;
                while (y < BoardWidth)
                {
                    board[x, y] = Empty; // 현재 위치에 채우기
                    y = y + 1; // y 좌표 하나씩 이동하면서 채우기
                }
                x = x + 1; // x 좌표 하나씩 이동하면서 채우기
            }

            // 외곽에 벽 세우기
            int wallX = 0;

            while (wallX < BoardWidth) // 위와 아래 벽 세우기
            {
                board[0, wallX] = Wall;
                board[BoardHeight - 1, wallX] = Wall;
                wallX = wallX + 1; // 한 칸씩 이동하면서 채우기
            }

            int wallY = 0;
            while (wallY < BoardHeight) // 좌측과 우측 벽 세우기
            {
                board[wallY, 0] = Wall;
                board[wallY, BoardWidth - 1] = Wall;
                wallY = wallY + 1;
            }

            // 먹이 생성
            board[food.X, food.Y] = Food;

            // 뱀 생성
            LinkedListNode<Pos> node = snake.First;
            while (node != null)
            {
                Pos p = node.Value;

                // 마지막 노드 확인 ( 머리 / 몸통 구분 )
                bool isHead = (node == snake.Last);

                if (isHead)
                {
                    board[p.X, p.Y] = Head;
                }
                else
                {
                    board[p.X, p.Y] = Body;
                }

                node = node.Next;
            }
        }

        // 매 프레임 전체 갱신
        private static void Update()
        {
            // 커서를 0,0 이동시킨 후 화면 덮어씌울 목적
            Console.SetCursorPosition(0, 0);

            // 보드 전체를 빈칸으로 리셋
            int x = 0;
            while (x < BoardHeight)
            {
                int y = 0;
                while (y < BoardWidth)
                {
                    board[x, y] = Empty; // 현재 위치에 채우기
                    y = y + 1; // y 좌표 하나씩 이동하면서 채우기
                }
                x = x + 1; // x 좌표 하나씩 이동하면서 채우기
            }
        }

        // 점수 / 조작법 표시
        private static void UpdateHUD()
        {
            Console.SetCursorPosition(0, BoardHeight);
            Console.Write("Score : ");
            Console.WriteLine(score);
            Console.Write("  ↑  \n");
            Console.Write("← ↓ →  Move : Arrow Keys");
        }
        
    }
}
