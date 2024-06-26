namespace Kdevaulo.WheelOfFortune.TimerBehaviour
{
    public class TimerController : BaseStateHandler
    {
        private readonly float _generationDelay;
        private readonly float _cooldownDuration;

        private readonly Timer _timer;

        private ITimerFinishHandler[] _finishHandlers;
        private ITimerTickHandler[] _tickHandlers;

        public TimerController(Settings settings)
        {
            int generationAttempts = settings.CooldownTickTimes;
            _generationDelay = settings.GenerationDelayInSeconds;
            _cooldownDuration = generationAttempts * _generationDelay;

            _timer = new Timer();
            _timer.Ticked += HandleTimerTick;
            _timer.Finished += HandleTimerFinished;
        }

        public override void HandleCooldownState()
        {
            _timer.Start(_cooldownDuration, _generationDelay);
        }

        public void SetFinishHandlers(params ITimerFinishHandler[] timerFinishHandlers)
        {
            _finishHandlers = timerFinishHandlers;
        }

        public void SetTickHandlers(params ITimerTickHandler[] timerTickHandlers)
        {
            _tickHandlers = timerTickHandlers;
        }

        private void HandleTimerFinished()
        {
            foreach (var handler in _finishHandlers)
            {
                handler.HandleFinish();
            }

            SwitchState(State.Active);
        }

        private void HandleTimerTick()
        {
            foreach (var handler in _tickHandlers)
            {
                handler.HandleTick();
            }
        }
    }
}