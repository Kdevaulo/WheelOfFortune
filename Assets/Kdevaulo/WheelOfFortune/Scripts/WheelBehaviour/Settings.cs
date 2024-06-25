using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    [CreateAssetMenu(menuName = nameof(WheelBehaviour) + "/" + nameof(Settings), fileName = nameof(Settings))]
    public class Settings : ScriptableObject
    {
        [field: Header("Generation")]
        [field: SerializeField] public int MinValue { get; private set; } = 5;
        [field: SerializeField] public int MaxValue { get; private set; } = 100;
        [field: SerializeField] public int Step { get; private set; } = 5;
        [field: SerializeField] public int CooldownTickTimes { get; private set; } = 10;
        [field: SerializeField] public float GenerationDelayInSeconds { get; private set; } = 1;
        [field: SerializeField] public Reward[] Rewards { get; private set; }

        [field: Header("Rotation")]
        [field: SerializeField] public int RotationDurationInSeconds { get; private set; } = 5;
        [field: SerializeField] public int MaxSpeed { get; private set; }
        [field: SerializeField] public AnimationCurve SpeedGraph { get; private set; }
    }
}