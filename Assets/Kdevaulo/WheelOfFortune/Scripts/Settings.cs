using UnityEngine;

namespace Kdevaulo.WheelOfFortune
{
    [CreateAssetMenu(menuName = nameof(WheelOfFortune) + "/" + nameof(Settings), fileName = nameof(Settings))]
    public class Settings : ScriptableObject
    {
        [field: Header("Generation")]
        [field: SerializeField] public int MinValue { get; private set; } = 5;
        [field: SerializeField] public int MaxValue { get; private set; } = 100;
        [field: SerializeField] public int Step { get; private set; } = 5;
        [field: SerializeField] public int CooldownTickTimes { get; private set; } = 10;
        [field: SerializeField] public float GenerationDelayInSeconds { get; private set; } = 1f;
        [field: SerializeField] public Reward[] Rewards { get; private set; }

        [field: Header("Rotation")]
        [field: SerializeField] public float RotationDurationInSeconds { get; private set; } = 5f;
        [field: SerializeField] public int MaxSpeed { get; private set; }
        [field: SerializeField] public AnimationCurve SpeedGraph { get; private set; }

        [field: Header("Reward")]
        [field: SerializeField] public Vector2 RewardStopTimeInSeconds { get; private set; } = new Vector2(1f, 2.5f);
        [field: SerializeField] public float RewardAppearTimeInSeconds { get; private set; } = 1f;
        [field: SerializeField] public float RewardDisappearTimeInSeconds { get; private set; } = 1f;
        [field: SerializeField] public float DelayAfterRewardInSeconds { get; private set; } = 2f;
    }
}