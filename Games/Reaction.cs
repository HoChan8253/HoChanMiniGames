using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public class Reaction : IGame
    {
        public string Id => "reaction_game";
        public string Title => "반응속도 테스트";

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("총 3번의 반응 속도를 측정합니다.\n");
            Console.Write("잠시 후 ");
            Effects.PrintColor("'지금!'", ConsoleColor.Yellow);
            Console.WriteLine(" 이 뜨면 ");
            Effects.PrintColor("엔터", ConsoleColor.Yellow);
            Console.WriteLine("를 바로 누르세요!");
            Console.WriteLine(" ( 3초 이내에 누르지 않으면 실패 )\n");
            Console.WriteLine("준비 되셨으면 아무 키나 입력해주세요.");
            Console.ReadKey();

            List<double> result = new List<double>(); // 3번 측정 값을 담을 List
            Random rnd = new Random();

            #region 수정 전
            //Console.WriteLine("준비...");
            //Thread.Sleep(1000);

            //Random rnd = new Random();
            //Thread.Sleep(rnd.Next(2000, 8000));

            //Effects.PrintColor("'지금!'\n", ConsoleColor.Yellow);
            //DateTime start = DateTime.Now;
            #endregion

            for ( int round = 1; round <= 3; round++ )
            {
                Console.Clear();
                Console.WriteLine($"Round {round} / 3");
                Effects.PrintColor("준비...", ConsoleColor.Red);
                Thread.Sleep(1000);

                Thread.Sleep(rnd.Next(2000, 8000));

                Effects.PrintColor("지금!", ConsoleColor.Yellow);
                DateTime start = DateTime.Now;

                Console.ReadLine();
                double ms = (DateTime.Now - start).TotalMilliseconds;
                result.Add(ms);

                Console.WriteLine($"반응 속도 : {ms:F0}"); // 소수점 아래 표시 안함
                Thread.Sleep(1500);
            }

            double average = 0;
            foreach (double ms in result)
            {
                average += ms;
            }
            average = average / result.Count;

            Console.Clear();
            Console.WriteLine("=====결과=====");

            for ( int i = 0; i < result.Count; i++ )
            {
                Console.WriteLine($"Round {i + 1}: {result[i]:F0}ms");
            }
            Console.WriteLine($"평균 반응속도: {average:F0}ms");

            string rank = // 삼항 연산자
                average < 200 ? "신급!" :
                average < 300 ? "보통" :
                average < 400 ? "느림" :
                "거북이";
            Console.Write("당신의 등급은 ");
            Effects.PrintColor($"{rank}", ConsoleColor.Yellow);
            Console.WriteLine(" 입니다.");

            Console.WriteLine();
            Console.WriteLine("엔터를 눌러주세요.");
            Console.ReadLine();
        }
    }
}
