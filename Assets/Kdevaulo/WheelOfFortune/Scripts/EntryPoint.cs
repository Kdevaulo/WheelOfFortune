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

        private StateHandler _stateHandler;

        private void Awake()
        {
            var rewardModel = new RewardSlotModel(_wheelView);
            var timerController = new TimerController(_settings);
            var buttonController = new ButtonController(_buttonView, _settings);
            var wheelController = new WheelController(_wheelView, _settings, rewardModel);
            var chooseRewardStateHandler = new ChooseRewardStateHandler(_wheelView, _settings, rewardModel);

            _stateHandler = new StateHandler(_buttonView, chooseRewardStateHandler, timerController,
                buttonController, wheelController);

            timerController.SetTickHandlers(wheelController, buttonController);
            timerController.SetFinishHandlers(buttonController);
        }

        private void Start()
        {
            _stateHandler.Start();
        }
    }
}