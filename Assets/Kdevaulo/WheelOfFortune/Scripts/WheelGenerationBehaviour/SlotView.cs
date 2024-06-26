using TMPro;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelGenerationBehaviour
{
    [AddComponentMenu(nameof(SlotView) + " in " + nameof(WheelGenerationBehaviour))]
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textHolder;
        [SerializeField] private TextMeshProUGUI _outlineTextHolder;

        [SerializeField] private float _targetWheelRotation;

        public void SetText(string text)
        {
            _textHolder.text = text;
            _outlineTextHolder.text = text;
        }

        public float GetWheelRotation()
        {
            return _targetWheelRotation;
        }

        public Vector2 GetPosition()
        {
            return _textHolder.rectTransform.position;
        }
    }
}