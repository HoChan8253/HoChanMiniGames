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
        private static void shuffle<T>(List<T> list)
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

        /*public static void FisherYatesShuffle(int[] inputArr)
        {
            Random random = new Random();

            for (int i = inputArr.Length - 1; i > 0; i--) //인덱스 역순
            {
                int j = random.Next(0, i + 1); // 0부터 i까지 랜덤 인덱스 선택

                // 배열 요소 교환
                int temp = inputArr[i];
                inputArr[i] = inputArr[j];
                inputArr[j] = temp;
            }
        }*/
    }
}
