using UnityEngine.Assertions;

namespace Kdevaulo.WheelOfFortune.WheelGenerationBehaviour
{
    public class NumbersGenerator
    {
        public int[] CurrentValues { get; private set; }

        private int[] _sequence;

        public void Initialize(int min, int step, int numbersCount)
        {
            _sequence = new int[numbersCount];

            for (int i = 0, n = min; i < numbersCount; i++, n += step)
            {
                _sequence[i] = n;
            }
        }

        public int[] GenerateRandomValues(int count)
        {
            Assert.IsTrue(count <= _sequence.Length);

            _sequence.Shuffle();

            CurrentValues = new int[count];

            for (int i = 0; i < count; i++)
            {
                CurrentValues[i] = _sequence[i];
            }

            return CurrentValues;
        }
    }
}