using System.Collections.Generic;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WheelOfFortune.WheelGenerationBehaviour;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune.RewardChooseBehaviour
{
    public class ChooseRewardStateHandler : BaseStateHandler
    {
        private readonly WheelView _view;
        private readonly Settings _settings;
        private readonly RewardFactory _rewardFactory;
        private readonly RewardSlotModel _rewardSlotModel;

        private Dictionary<RewardView, int> _rewards = new Dictionary<RewardView, int>();

        private int _currentRewardPoints;

        private CancellationTokenSource _cts;

        public ChooseRewardStateHandler(WheelView view, Settings settings, RewardSlotModel rewardSlotModel)
        {
            _view = view;
            _settings = settings;
            _rewardSlotModel = rewardSlotModel;

            _rewardFactory = new RewardFactory(_settings.RewardView, _view.SpawnableRewardsContainer);
        }

        public override void HandleChoosingRewardState()
        {
            _currentRewardPoints = 0;

            int index = Random.Range(0, _rewardSlotModel.SlotsCount);
            int rewardValue = _rewardSlotModel.GetRewardValue(index);
            Debug.Log($"RewardValue = {rewardValue}");

            float targetRotation = _rewardSlotModel.GetTargetRotation(index);
            _rewardFactory.Initialize(_rewardSlotModel.Reward.Sprite);

            RefreshToken();

            HandleRewardChooseAsync(targetRotation, index, _view.GetFinishPosition(), rewardValue, _cts.Token)
                .Forget();
        }

        private async UniTask AppearItemsAsync(Vector2 targetPosition, CancellationToken token)
        {
            float appearDuration = _settings.RewardAppearTimeInSeconds;
            var radiusInUnits = _settings.RewardRadiusInUnits;

            var operations = new List<UniTask>();

            foreach (var reward in _rewards)
            {
                var randomPosition = Utilities.GenerateRandomPosition(targetPosition, radiusInUnits.x, radiusInUnits.y);
                var operation = reward.Key.AppearAsync(appearDuration, randomPosition, token);
                operations.Add(operation);
            }

            await UniTask.WhenAll(operations);
        }

        private void CreateRewardItems(int rewardValue, int maxRewardsCount, Vector2 position)
        {
            int[] rewardsValues = Utilities.DistributeEvenly(rewardValue, maxRewardsCount);

            foreach (int value in rewardsValues)
            {
                var view = _rewardFactory.Create();
                _rewards.Add(view, value);
                view.SetPosition(position);
            }
        }

        private void HandleRewardAddition(int value)
        {
            _currentRewardPoints += value;

            _view.SetRewardText(_currentRewardPoints.ToString());
        }

        private async UniTask HandleRewardChooseAsync(float targetRotation, int itemIndex, Vector2 finishPosition,
            int rewardValue, CancellationToken token)
        {
            await _view.RotateAsync(targetRotation, _settings.RotationDurationInSeconds, _settings.AnimationCurve,
                token);

            _view.DisableRewardImage();
            _view.EnableRewardText();
            _view.SetRewardText("0");

            var rewardValuePosition = _rewardSlotModel.GetTargetPosition(itemIndex);

            CreateRewardItems(rewardValue, _settings.MaxRewardsCount, rewardValuePosition);
            await AppearItemsAsync(rewardValuePosition, token);
            await MoveToFinishAsync(finishPosition, token);
            await UniTask.WaitForSeconds(_settings.DelayAfterRewardInSeconds, cancellationToken: token);
            RemoveItems();

            SwitchState(State.Cooldown);
        }

        private async UniTask MoveToFinishAsync(Vector2 targetPosition, CancellationToken token)
        {
            float disappearDuration = _settings.RewardDisappearTimeInSeconds;
            var delayTime = _settings.RewardStopTimeInSeconds;

            var operations = new List<UniTask>();

            foreach (var reward in _rewards)
            {
                float delay = Random.Range(delayTime.x, delayTime.y);

                var operation = reward.Key.MoveToFinishAsync(delay, disappearDuration, targetPosition, token)
                    .ContinueWith(() => HandleRewardAddition(reward.Value));

                operations.Add(operation);
            }

            await UniTask.WhenAll(operations);
        }

        private void RefreshToken()
        {
            if (_cts is { IsCancellationRequested: false })
            {
                _cts.Cancel();
                _cts.Dispose();
            }

            _cts = new CancellationTokenSource();
        }

        private void RemoveItems()
        {
            foreach (var reward in _rewards)
            {
                Object.Destroy(reward.Key.gameObject);
            }

            _rewards.Clear();
        }
    }
}