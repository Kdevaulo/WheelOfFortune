namespace Kdevaulo.WheelOfFortune.UIBehaviour
{
    public class ButtonController : BaseStateHandler, ITimerTickHandler, ITimerFinishHandler
    {
        private readonly IUserActionsProvider _actionsProvider;

        private readonly Settings _settings;
        private readonly ButtonView _view;

        private int _ticksLeft;

        public ButtonController(ButtonView view, Settings settings)
        {
            _view = view;
            _settings = settings;
        }

        void ITimerFinishHandler.HandleFinish()
        {
            _view.SetTimerText(string.Empty);
            _view.DisableTimerText();
            _view.EnableAppealText();
        }

        void ITimerTickHandler.HandleTick()
        {
            _view.SetTimerText(_ticksLeft.ToString());
            --_ticksLeft;
        }

        public override void HandleActiveState()
        {
            _view.EnableButton();
        }

        public override void HandleChoosingRewardState()
        {
            _view.DisableButton();
        }

        public override void HandleCooldownState()
        {
            _ticksLeft = _settings.CooldownTickTimes - 1;
            _view.SetTimerText(_settings.CooldownTickTimes.ToString());
            _view.EnableTimerText();
            _view.DisableAppealText();
            _view.DisableButton();
        }
    }
}