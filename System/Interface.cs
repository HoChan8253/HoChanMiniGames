using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public interface IGame // 모든 미니게임이 따라야 하는 최소 규칙
    {
        string Id { get; }
        string Title { get; }
        void Run();
    }

    public enum NextAction
    {
        Retry,
        Menu
    }

    public struct RoundOut
    {
        public bool correct;
        public NextAction Next;
    }
}
