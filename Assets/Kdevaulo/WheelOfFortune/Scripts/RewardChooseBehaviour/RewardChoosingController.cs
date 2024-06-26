using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WheelOfFortune.WheelGenerationBehaviour;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Kdevaulo.WheelOfFortune.RewardChooseBehaviour
{
    public class RewardChoosingController
    {
        private readonly WheelView _view;
        private readonly Settings _settings;
        private readonly RewardModel _rewardModel;
        private readonly IUserActionsProvider _actionsProvider;

        private CancellationTokenSource _cts;

        private int _startSpeed;
        private AnimationCurve _animationCurve;
        private float _rotationDuration;
        private float _delayAfterReward;
        private Vector2 _rewardStopTimeInSeconds;
        private float _rewardAppearTimeInSeconds;
        private float _rewardDisappearTimeInSeconds;

        public RewardChoosingController(WheelView view, Settings settings, RewardModel rewardModel,
            IUserActionsProvider actionsProvider)
        {
            _view = view;
            _settings = settings;
            _rewardModel = rewardModel;
            _actionsProvider = actionsProvider;

            _animationCurve = settings.AnimationCurve;
            _rotationDuration = settings.RotationDurationInSeconds;
            _delayAfterReward = settings.DelayAfterRewardInSeconds;
            _rewardStopTimeInSeconds = settings.RewardStopTimeInSeconds;
            _rewardAppearTimeInSeconds = settings.RewardAppearTimeInSeconds;
            _rewardDisappearTimeInSeconds = settings.RewardDisappearTimeInSeconds;

            _actionsProvider.ButtonClicked += HandleButtonClick;
        }

        private void HandleButtonClick()
        {
            int index = Random.Range(0, _rewardModel.SlotsCount);
            int rewardValue = _rewardModel.GetRewardValue(index);
            Debug.Log($"RewardValue = {rewardValue}");

            float targetRotation = _rewardModel.GetTargetRotation(index);

            RefreshToken();

            HandleRewardChooseAsync(targetRotation, _cts.Token).Forget();
        }

        private async UniTask HandleRewardChooseAsync(float targetRotation, CancellationToken token)
        {
            await _view.RotateAsync(targetRotation, _rotationDuration, _animationCurve, token);
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
    }
}