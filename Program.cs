using HoChanMiniGames.Games;
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
            var manager = new GameManager();

            manager.Register(new UpAndDown());
            manager.Register(new RPS());
            manager.Register(new NumBaseball());
            manager.Register(new Reaction());
            //manager.Register(new Quiz());

            manager.Run();
        }
    }
}
