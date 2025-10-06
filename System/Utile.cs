using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoChanMiniGames
{
    public class Utile
    {
        // Fisher - Yates shuffle
        public static void Shuffle<T>(List<T> list)
        {
            var rnd = new Random();

            for ( int i = list.Count - 1; i > 0; i-- )
            {
                int j = rnd.Next(i + 1); // 0 부터 i 까지 랜덤 인덱스 선택
                
                // Swap
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
