using Kdevaulo.WheelOfFortune.RewardChooseBehaviour;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune
{
    [CreateAssetMenu(menuName = nameof(WheelOfFortune) + "/" + nameof(Settings), fileName = nameof(Settings))]
    public class Settings : ScriptableObject
    {
        [field: Header("WheelGeneration")]
        [field: SerializeField] public int MinValue { get; private set; } = 5;
        [field: SerializeField] public int PossibleNumbersCount { get; private set; } = 20;
        [field: SerializeField] public int Step { get; private set; } = 5;
        [field: SerializeField] public int CooldownTickTimes { get; private set; } = 10;
        [field: SerializeField] public float GenerationDelayInSeconds { get; private set; } = 1f;
        [field: SerializeField] public Reward[] Rewards { get; private set; }

        [field: Header("WheelRotation")]
        [field: SerializeField] public float RotationDurationInSeconds { get; private set; } = 5f;
        [field: SerializeField] public AnimationCurve AnimationCurve { get; private set; }

        [field: Header("RewardAnimation")]
        [field: SerializeField] public Vector2 RewardStopTimeInSeconds { get; private set; } = new Vector2(1f, 2.5f);
        [field: SerializeField] public float RewardAppearTimeInSeconds { get; private set; } = 1f;
        [field: SerializeField] public float RewardDisappearTimeInSeconds { get; private set; } = 1f;
        [field: SerializeField] public float DelayAfterRewardInSeconds { get; private set; } = 2f;

        [field: Header("RewardGeneration")]
        [field: SerializeField] public int MaxRewardsCount { get; private set; } = 20;
        [field: SerializeField] public Vector2 RewardRadiusInUnits { get; private set; }
        [field: SerializeField] public RewardView RewardView { get; private set; }
    }
}