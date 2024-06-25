using Kdevaulo.WheelOfFortune.WheelGenerationBehaviour;

using UnityEngine.Assertions;

namespace Kdevaulo.WheelOfFortune
{
    public class RewardModel
    {
        public int SlotsCount { get; private set; }

        private SlotView[] _slotViews;

        private int[] _values;

        private string _rewardId;

        public RewardModel(WheelView view)
        {
            _slotViews = view.GetSlotViews();
            SlotsCount = _slotViews.Length;
        }

        public void SetValues(int[] values)
        {
            _values = values;
        }

        public void SetReward(string rewardId)
        {
            _rewardId = rewardId;
        }

        public int GetRewardValue(int index)
        {
            Assert.IsTrue(index >= 0 && index <= _values.Length);

            return _values[index];
        }

        public float GetTargetRotation(int index)
        {
            return _slotViews[index].GetWheelRotation();
        }
    }
}