using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.WheelOfFortune
{
    public static class Utilities
    {
        public static int[] DistributeEvenly(int totalValue, int maxItemsCount)
        {
            int[] rewards;

            int rewardPerObject = totalValue / maxItemsCount;
            int remainingReward = totalValue % maxItemsCount;

            if (totalValue <= maxItemsCount)
            {
                rewards = new int[totalValue];

                for (int i = 0; i < totalValue; i++)
                {
                    rewards[i] = 1;
                }
            }
            else
            {
                rewards = new int[maxItemsCount];

                for (int i = 0; i < maxItemsCount; i++)
                {
                    rewards[i] = rewardPerObject;
                }

                for (int i = 0; i < remainingReward; i++)
                {
                    rewards[i]++;
                }
            }

            return rewards;
        }

        public static Vector2 GenerateRandomPosition(Vector2 center, float minRadius, float maxRadius)
        {
            Assert.IsTrue(minRadius >= 0);
            Assert.IsTrue(maxRadius >= 0);
            Assert.IsTrue(maxRadius >= minRadius);

            float angle = Random.Range(0f, Mathf.PI * 2);
            float radius = Random.Range(minRadius, maxRadius);

            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y + radius * Mathf.Sin(angle);

            return new Vector2(x, y);
        }

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