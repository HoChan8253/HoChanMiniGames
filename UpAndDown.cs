using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public class UpAndDown : IGame
    {
        public string Id => "up_and_down";
        public string Title => "UP & DOWN";

        public void Run()
        {
            var rnd = new Random();
            int cpuChoose = rnd.Next(1, 101);
            int tries = 0;
            int maxTries = 7;
            bool success = false;

            Console.WriteLine("1부터 100사이의 숫자를 맞춰보세요");
            Console.WriteLine("기회는 7번 입니다.");

            while ( tries < maxTries )
            {
                tries++;
                int n = Input.ReadIntRange($"{tries}번째 입력: ", 1, 100);

                if ( n == cpuChoose )
                {
                    Console.WriteLine("정답입니다!");
                    Console.WriteLine($"시도 횟수: {tries}");
                    break;
                    
                }
                Console.WriteLine( n < cpuChoose ? "UP!" : "DOWN!");
            }

            if ( !success )
            {
                Console.WriteLine($"실패! 아쉽습니다. 정답은 {cpuChoose}였습니다.");
            }
        }
    }
}
