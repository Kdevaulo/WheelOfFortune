using Kdevaulo.WheelOfFortune.RewardChooseBehaviour;
using Kdevaulo.WheelOfFortune.TimerBehaviour;
using Kdevaulo.WheelOfFortune.UIBehaviour;
using Kdevaulo.WheelOfFortune.WheelGenerationBehaviour;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune
{
    [AddComponentMenu(nameof(EntryPoint) + " in " + nameof(WheelOfFortune))]
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private ButtonView _buttonView;

        private WheelController _wheelController;
        private TimerController _timerController;
        private ButtonController _buttonController;
        private RewardChoosingController _rewardChoosingController;

        private IClearable[] _clearables;

        private void Awake()
        {
            var rewardModel = new RewardSlotModel(_wheelView);
            _timerController = new TimerController(_settings);
            _buttonController = new ButtonController(_buttonView, _settings, _buttonView);
            _wheelController = new WheelController(_wheelView, _settings, rewardModel);
            _rewardChoosingController = new RewardChoosingController(_wheelView, _settings, rewardModel, _buttonView);

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