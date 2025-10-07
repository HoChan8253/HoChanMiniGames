using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace HoChanMiniGames
{
    public class MemoryMatch : IGame
    {
        public string Id => "memory_match";
        public string Title => "카드 짝 맞추기";

        static int Width = 6; // 가로 
        static int Height = 6; // 세로

        // 6 x 6 = 36 장 ( 나중에 추가하고 싶으면 가로 세로만 값 수정하면 된다. )
        static int PairCount = (Width * Height) / 2;

        // 덱에 저장할 다양만 문자들 ( 이 중 18개만 랜덤으로 선택 )
        static readonly char[] deckPool = new char[]
        {
            '♠','♤','♥','♡','★','☆','◎','♣','♧','Ω',
            'A','B','C','D','Ψ','♂','♀','∇','ⓐ','ⓑ','ⓒ','ⓓ',
            '※','▣','♬','＠','＆','㉿','㏇','㉾'
        };

        // 2차원 배열
        static char[,] board; // 문자
        static bool[,] release; // 이번 턴 공개
        static bool[,] matched; // 맞춘 칸 ( 영구 공개 )

        static int turns = 0;

        // 보드 관련 배열 생성 / 초기화
        static void spawnArray()
        {
            // 정답판
            board = new char[Width, Height];

            // 이번턴 공개
            release = new bool[Width, Height];

            // 영구 공개
            matched = new bool[Width, Height];
        }

        static List<char> BuildDeck()
        {
            // deckPool 의 특수문자들에서 18개 가져와서 덱 만듬
            List<char> deck = new List<char>();

            int i = 0;

            while ( i < PairCount )
            {
                char symbol = deckPool[i];
                deck.Add(symbol);
                deck.Add(symbol);
                i++;
            }
            return deck;
        }
        
        // 보드에 덱을 채우기
        static void FillBoard(List<char> deck)
        {
            int index = 0;
            
            for (int w = 0; w < Width; w++)
            {
                for(int h = 0; h < Height; h++)
                {
                    board[w, h] = deck[index];
                    index++;
                }
            }
        }

        static bool IsInBoard(int w, int h) // 보드 경계 체크
        {
            if (w < 0 || w >= Width)
            {
                return false;
            }

            if (h < 0)
            {
                return false;
            }

            if (h >= Height)
            {
                return false;
            }

            return true; // 4 가지 모두 통과 = 보드 안
        }

        static bool IsAllMatched() // 모든 칸 매칭 여부 체크
        {
            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    if (!matched[w, h])
                    {
                        return false; // 다 맞추지 못했다면 false
                    }
                }
            }
            return true;
        }

        static void PrintBoard() // 보드 출력 ( 가려진 칸은 물음표 )
        {
            Console.Write("======");
            Effects.PrintColor("Memory Match", ConsoleColor.Yellow);
            Console.WriteLine("======");

            Console.Write("   ");

            for (int w = 0; w < Width; w++)
            {
                Effects.PrintColor((w + " "), ConsoleColor.Blue); // 한 칸씩 띄우며 출력 ( 0 1 2 3 )
            }

            Console.WriteLine();

            for (int h = 0; h < Height; h++)
            {
                Effects.PrintColor((h +"  "), ConsoleColor.Yellow);

                for ( int w = 0; w < Width; w++)
                {
                    if (matched[w, h] || release[w, h]) // 맞춘 카드거나, 현재 공개된 카드라면 문자를 출력
                    {
                        Console.Write(board[w, h] + " ");
                    }
                    else
                    {
                        Console.Write("? ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void Run()
        {
            spawnArray(); // 보드 배열 준비

            List<char> deck = BuildDeck(); // 덱 구성

            Utile.Shuffle(deck); // 피셔 예이츠 셔플

            FillBoard(deck); // 보드 채워넣기

            RunGameLoop();
        }

        static void RunGameLoop()
        {
            while(!IsAllMatched())
            {
                Console.Clear();
                PrintBoard();

                // 첫번째 좌표 입력 ( x , y )
                Console.WriteLine("\n게임을 종료하려면 Q를 입력하세요.");
                string firstInput = Console.ReadLine()?.Trim();

                if (firstInput?.ToUpper() == "Q")
                {
                    Effects.PrintColor("현재 게임을 종료합니다.\n", ConsoleColor.Yellow);
                    Console.ReadKey();
                    return;
                }

                int x1 = 0;
                int y1 = 0;
                if (!Input.ReadCoordXY(out x1, out y1, Width, Height))
                {
                    continue; // 입력 실패시 처음으로 돌아감
                }

                int w1 = x1;
                int h1 = y1;

                // 좌표 유효성 추가 체크
                if (!IsInBoard(w1, h1))
                {
                    Effects.PrintColor("잘못된 좌표입니다.\n", ConsoleColor.Red);
                    Console.WriteLine("아무 키나 눌러주세요.");
                    Console.ReadKey(true);
                    continue;
                }

                if (matched[w1, h1])
                {
                    Effects.PrintColor("이미 맞춘 칸입니다.\n", ConsoleColor.Green);
                    Console.WriteLine("아무 키나 눌러주세요.");
                    Console.ReadKey(true);
                    continue;
                }

                release[w1, h1] = true; // 첫 칸 임시 공개

                Console.Clear();
                PrintBoard();

                // 두번째 좌표 입력 ( x , y )
                int x2 = 0;
                int y2 = 0;
                if (!Input.ReadCoordXY(out x2, out y2, Width, Height))
                {
                    release[w1, h1] = false;
                    continue;
                }

                int w2 = x2;
                int h2 = y2;

                // 좌표 유효성 추가 체크
                if (!IsInBoard(w2, h2))
                {
                    release[w1, h1] = false;
                    Effects.PrintColor("잘못된 좌표입니다.\n", ConsoleColor.Red);
                    Console.WriteLine("아무 키나 눌러주세요.");
                    Console.ReadKey(true);
                    continue;
                }

                // 같은 칸 두번 선택 방지
                if (w1 == w2 && h1 == h2)
                {
                    release[w1, h1] = false;
                    Effects.PrintColor("같은 칸을 두번 선택할 수 없습니다.\n", ConsoleColor.Yellow);
                    Console.WriteLine("아무 키나 눌러주세요.");
                    continue;
                }

                // 이미 맞춘 칸이면 무효
                if (matched[w2, h2])
                {
                    release[w1, h1] = false;
                    Effects.PrintColor("이미 맞춘 칸입니다.\n", ConsoleColor.Green);
                    Console.WriteLine("아무 키나 눌러주세요.");
                    continue;
                }

                release[w2, h2] = true;

                Console.Clear();
                PrintBoard();

                // 좌표 두개 받아 비교 및 처리

                turns++;

                if (board[w1, h1] == board[w2, h2])
                {
                    // 매칭 성공 ( 영구 공개 )
                    matched[w1, h1] = true;
                    matched[w2, h2] = true;

                    Effects.PrintColor("\n매칭 성공!\n", ConsoleColor.Green);
                    Console.WriteLine("아무 키나 눌러주세요.");
                    Console.ReadKey(true);
                }
                else
                {
                    Effects.PrintColor("\n매칭 실패...\n", ConsoleColor.Red);
                    release[w1, h1] = false;
                    release[w2, h2] = false; // 임시 공개 해제
                    
                    Console.WriteLine("아무 키나 눌러주세요.");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
