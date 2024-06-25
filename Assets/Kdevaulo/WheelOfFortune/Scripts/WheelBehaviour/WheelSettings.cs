using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    [CreateAssetMenu(menuName = nameof(WheelBehaviour) + "/" + nameof(WheelSettings), fileName = nameof(WheelSettings))]
    public class WheelSettings : ScriptableObject
    {
        [field: Header("Generation")]
        [field: SerializeField] public int MinValue { get; private set; }
        [field: SerializeField] public int Step { get; private set; }
        [field: SerializeField] public int GenerationAttempts { get; private set; }
        [field: SerializeField] public float GenerationDelayInSeconds { get; private set; }

        [field: Header("Rotation")]
        [field: SerializeField] public int RotationDurationInSeconds { get; private set; }
        [field: SerializeField] public int MaxSpeed { get; private set; }
        [field: SerializeField] public AnimationCurve SpeedGraph { get; private set; }
    }
}