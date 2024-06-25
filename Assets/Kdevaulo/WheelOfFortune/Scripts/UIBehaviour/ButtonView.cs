using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.WheelOfFortune.UIBehaviour
{
    [AddComponentMenu(nameof(ButtonView) + " in " + nameof(UIBehaviour))]
    public class ButtonView : MonoBehaviour, IUserActionsProvider
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _appealTextContainer;

        [SerializeField] private TextMeshProUGUI _timerTextHolder;
        [SerializeField] private TextMeshProUGUI _timerTextOutlineHolder;

        private void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(HandleButtonClick);
        }

        public event Action ButtonClicked = delegate { };

        public void SetTimerText(string text)
        {
            _timerTextHolder.text = text;
            _timerTextOutlineHolder.text = text;
        }

        public void EnableTimerText()
        {
            _timerTextHolder.gameObject.SetActive(true);
            _timerTextOutlineHolder.gameObject.SetActive(true);
        }

        public void DisableTimerText()
        {
            _timerTextHolder.gameObject.SetActive(false);
            _timerTextOutlineHolder.gameObject.SetActive(false);
        }

        public void EnableAppealText()
        {
            _appealTextContainer.SetActive(true);
        }

        public void DisableAppealText()
        {
            _appealTextContainer.SetActive(false);
        }

        public void EnableButton()
        {
            _button.interactable = true;
        }

        public void DisableButton()
        {
            _button.interactable = false;
        }

        private void HandleButtonClick()
        {
            ButtonClicked.Invoke();
        }
    }
}