using TMPro;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    [AddComponentMenu(nameof(SlotView) + " in " + nameof(WheelBehaviour))]
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textHolder;
        [SerializeField] private TextMeshProUGUI _outlineTextHolder;

        public void SetText(string text)
        {
            _textHolder.text = text;
            _outlineTextHolder.text = text;
        }
    }
}