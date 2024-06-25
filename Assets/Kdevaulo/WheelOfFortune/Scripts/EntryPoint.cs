using System;

using Kdevaulo.WheelOfFortune.UIBehaviour;
using Kdevaulo.WheelOfFortune.WheelBehaviour;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune
{
    [AddComponentMenu(nameof(EntryPoint) + " in " + nameof(WheelOfFortune))]
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private Settings _settings;
        [SerializeField] private ButtonView _buttonView;

        private WheelController _wheelController;
        private TimerController _timerController;
        private ButtonController _buttonController;

        private IClearable[] _clearables;

        private void Awake()
        {
            _timerController = new TimerController(_settings);
            _wheelController = new WheelController(_wheelView, _settings);
            _buttonController = new ButtonController(_buttonView, _settings);

            _timerController.SetTickHandlers(_wheelController, _buttonController);
            _timerController.SetFinishHandlers(_buttonController);

            _clearables = new IClearable[] { };
        }

        private void Start()
        {
            _buttonController.Initialize();
            _timerController.Start();
        }

        private void OnDestroy()
        {
            foreach (var item in _clearables)
            {
                item.Clear();
            }
        }
    }
}