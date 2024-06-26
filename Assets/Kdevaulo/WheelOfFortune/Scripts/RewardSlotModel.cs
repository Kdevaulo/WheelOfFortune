using Kdevaulo.WheelOfFortune.WheelGenerationBehaviour;

using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.WheelOfFortune
{
    public class RewardSlotModel
    {
        public Reward Reward { get; private set; }
        public int SlotsCount { get; private set; }

        private SlotView[] _slotViews;

        private int[] _values;

        public RewardSlotModel(WheelView view)
        {
            _slotViews = view.GetSlotViews();
            SlotsCount = _slotViews.Length;
        }

        public int GetRewardValue(int index)
        {
            Assert.IsTrue(index >= 0 && index <= _values.Length);

            return _values[index];
        }

        public Vector2 GetTargetPosition(int index)
        {
            return _slotViews[index].GetPosition();
        }

        public float GetTargetRotation(int index)
        {
            return _slotViews[index].GetWheelRotation();
        }

        public void SetReward(Reward reward)
        {
            Reward = reward;
        }

        public void SetValues(int[] values)
        {
            _values = values;
        }
    }
}