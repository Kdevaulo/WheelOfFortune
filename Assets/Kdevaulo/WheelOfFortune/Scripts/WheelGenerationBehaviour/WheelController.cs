using System.Linq;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelGenerationBehaviour
{
    public class WheelController : ITimerTickHandler
    {
        private readonly WheelView _view;
        private readonly NumbersGenerator _generator;
        private readonly RewardModel _rewardModel;

        private readonly Reward[] _rewards;

        private readonly int _slotsCount;
        private readonly int _maxGenerationIndex;

        private int _currentGenerationIndex = 0;

        private string _lastRewardId = string.Empty;

        public WheelController(WheelView view, Settings settings, RewardModel rewardModel)
        {
            _view = view;
            _rewardModel = rewardModel;
            _generator = new NumbersGenerator();

            _slotsCount = _rewardModel.SlotsCount;
            _rewards = settings.Rewards;
            _maxGenerationIndex = settings.CooldownTickTimes;

            int numbersCount =
                Mathf.CeilToInt((settings.MaxValue - settings.MinValue) / (float) settings.Step);

            _generator.Initialize(settings.MinValue, settings.Step, numbersCount);
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

        private void SetValues()
        {
            int[] values = _generator.GenerateRandomValues(_slotsCount);
            _rewardModel.SetValues(values);
            _view.SetValues(values);
        }

        private void SetReward()
        {
            var targetRewards = _rewards.Where(x => x.Id != _lastRewardId).ToArray();
            int index = Random.Range(0, targetRewards.Length);
            var targetReward = targetRewards[index];

            _rewardModel.SetReward(targetReward.Id);
            _lastRewardId = targetReward.Id;
            _view.SetRewardSprite(targetReward.Sprite);
        }
    }
}