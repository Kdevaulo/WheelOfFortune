using System.Linq;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelGenerationBehaviour
{
    public class WheelController : BaseStateHandler, ITimerTickHandler
    {
        private readonly Reward[] _rewards;

        private readonly WheelView _view;
        private readonly NumbersGenerator _generator;
        private readonly RewardSlotModel _rewardSlotModel;

        private readonly int _slotsCount;
        private readonly int _maxGenerationIndex;

        private int _currentGenerationIndex;

        private string _lastRewardId = string.Empty;

        public WheelController(WheelView view, Settings settings, RewardSlotModel rewardSlotModel)
        {
            _view = view;
            _rewardSlotModel = rewardSlotModel;
            _generator = new NumbersGenerator();

            _slotsCount = _rewardSlotModel.SlotsCount;
            _rewards = settings.Rewards;
            _maxGenerationIndex = settings.CooldownTickTimes;

            _generator.Initialize(settings.MinValue, settings.Step, settings.PossibleNumbersCount);
        }

        void ITimerTickHandler.HandleTick()
        {
            if (++_currentGenerationIndex >= _maxGenerationIndex)
            {
                return;
            }

            SetValues();
            SetReward();
        }

        public override void HandleCooldownState()
        {
            _currentGenerationIndex = 0;

            SetValues();
            SetReward();

            _view.EnableRewardImage();
            _view.DisableRewardText();
        }

        private void SetReward()
        {
            var targetRewards = _rewards.Where(x => x.Id != _lastRewardId).ToArray();
            int index = Random.Range(0, targetRewards.Length);
            var targetReward = targetRewards[index];

            _rewardSlotModel.SetReward(targetReward);
            _lastRewardId = targetReward.Id;
            _view.SetRewardSprite(targetReward.Sprite);
        }

        private void SetValues()
        {
            int[] values = _generator.GenerateRandomValues(_slotsCount);
            _rewardSlotModel.SetValues(values);
            _view.SetValues(values);
        }
    }
}