using Kdevaulo.WheelOfFortune.WheelBehaviour;

namespace Kdevaulo.WheelOfFortune.UIBehaviour
{
    public class ButtonController : ITimerTickHandler, ITimerFinishHandler
    {
        private readonly ButtonView _view;
        private readonly Settings _settings;

        private int _ticksLeft;

        public ButtonController(ButtonView view, Settings settings)
        {
            _view = view;
            _settings = settings;
        }

        public void Initialize()
        {
            _ticksLeft = _settings.CooldownTickTimes - 1;
            _view.EnableTimerText();
            _view.DisableAppealText();
            _view.DisableButton();
        }

        void ITimerTickHandler.HandleTick()
        {
            _view.SetTimerText(_ticksLeft.ToString());
            --_ticksLeft;
        }

        void ITimerFinishHandler.HandleFinish()
        {
            _view.SetTimerText(string.Empty);
            _view.DisableTimerText();
            _view.EnableAppealText();
            _view.EnableButton();
        }
    }
}