using HoChanMiniGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);

            var manager = new GameManager();

            manager.Register(new Snake());
            manager.Register(new MemoryMatch());
            manager.Register(new UpAndDown());
            manager.Register(new RPS());
            manager.Register(new NumBaseball());
            manager.Register(new Reaction());
                        
            manager.Run();
        }
    }
}
