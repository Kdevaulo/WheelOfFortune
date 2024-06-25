using Random = UnityEngine.Random;

namespace Kdevaulo.WheelOfFortune
{
    public static class ArrayUtilities
    {
        public static void Shuffle(this int[] array)
        {
            int maxIndex = array.Length - 1;

            for (int i = maxIndex; i >= 0; i--)
            {
                int j = Random.Range(0, i + 1);

                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}