using UnityEngine;

namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    public class WheelController : ITimerTickHandler
    {
        private readonly WheelView _view;
        private readonly NumbersGenerator _generator;

        private readonly int _slotsCount;
        private readonly int _maxGenerationIndex;

        private int _currentGenerationIndex;

        public WheelController(WheelView view, Settings settings)
        {
            _view = view;
            _generator = new NumbersGenerator();

            _currentGenerationIndex = 0;
            _maxGenerationIndex = settings.CooldownTickTimes;
            _slotsCount = _view.GetSlotsCount();

            int numbersCount =
                Mathf.CeilToInt((settings.MaxValue - settings.MinValue) / (float) settings.Step);

            _generator.Initialize(settings.MinValue, settings.Step, numbersCount);
        }

        void ITimerTickHandler.HandleTick()
        {
            if (++_currentGenerationIndex >= _maxGenerationIndex)
            {
                return;
            }

            int[] values = _generator.GetRandomValues(_slotsCount);
            _view.SetValues(values);
        }
    }
}