using UnityEngine.Assertions;

namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    public class NumbersGenerator
    {
        private int[] _sequence;

        public void Initialize(int min, int step, int numbersCount)
        {
            _sequence = new int[numbersCount];

            for (int i = 0, n = min; i < numbersCount; i++, n += step)
            {
                _sequence[i] = n;
            }
        }

        public int[] GetRandomValues(int count)
        {
            Assert.IsTrue(count <= _sequence.Length);

            _sequence.Shuffle();

            int[] targetValues = new int[count];

            for (int i = 0; i < count; i++)
            {
                targetValues[i] = _sequence[i];
            }

            return targetValues;
        }
    }
}