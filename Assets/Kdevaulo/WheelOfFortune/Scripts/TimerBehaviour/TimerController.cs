namespace Kdevaulo.WheelOfFortune.TimerBehaviour
{
    public class TimerController
    {
        private readonly Settings _settings;
        private readonly Timer _timer;


        private readonly float _generationDelay;
        private readonly float _cooldownDuration;

        private ITimerTickHandler[] _tickHandlers;
        private ITimerFinishHandler[] _finishHandlers;


        public TimerController(Settings settings)
        {
            _settings = settings;

            int generationAttempts = _settings.CooldownTickTimes;
            _generationDelay = _settings.GenerationDelayInSeconds;
            _cooldownDuration = (generationAttempts - 1) * _generationDelay;

            _timer = new Timer();
            _timer.Ticked += HandleTimerTick;
            _timer.Finished += HandleTimerFinished;
        }

        public void SetTickHandlers(params ITimerTickHandler[] timerTickHandlers)
        {
            _tickHandlers = timerTickHandlers;
        }

        public void SetFinishHandlers(params ITimerFinishHandler[] timerFinishHandlers)
        {
            _finishHandlers = timerFinishHandlers;
        }

        public void Start()
        {
            HandleTimerTick();
            _timer.Start(_cooldownDuration, _generationDelay);
        }

        private void HandleTimerFinished()
        {
            foreach (var handler in _finishHandlers)
            {
                handler.HandleFinish();
            }
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