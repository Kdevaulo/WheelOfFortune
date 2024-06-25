using UnityEngine;

namespace Kdevaulo.WheelOfFortune.RewardChooseBehaviour
{
    public class RewardChoosingController
    {
        private readonly RewardModel _rewardModel;

        public RewardChoosingController(Settings settings, RewardModel rewardModel)
        {
            _rewardModel = rewardModel;

            int startSpeed = settings.MaxSpeed;
            var speedGraph = settings.SpeedGraph;
            float rotationDuration = settings.RotationDurationInSeconds;
            float delayAfterReward = settings.DelayAfterRewardInSeconds;
            var rewardStopTimeInSeconds = settings.RewardStopTimeInSeconds;
            float rewardAppearTimeInSeconds = settings.RewardAppearTimeInSeconds;
            float rewardDisappearTimeInSeconds = settings.RewardDisappearTimeInSeconds;
        }

        public void ChooseReward()
        {
            int index = Random.Range(0, _rewardModel.SlotsCount);
            int rewardValue = _rewardModel.GetRewardValue(index);
            Debug.Log($"RewardValue = {rewardValue}");

            float targetRotation = _rewardModel.GetTargetRotation(index);
        }
    }
}