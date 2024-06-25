namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    public class WheelController : IClearable
    {
        private readonly IUserActionsProvider _actionsProvider;

        private readonly WheelView _view;
        private readonly WheelSettings _settings;
        private readonly NumbersGenerator _generator;

        public WheelController(WheelView view, WheelSettings settings, IUserActionsProvider actionsProvider)
        {
            _view = view;
            _settings = settings;
            _actionsProvider = actionsProvider;

            _generator = new NumbersGenerator();

            _actionsProvider.ButtonClicked += HandleButtonClicked;

            _generator.Initialize(_settings.MinValue, _settings.Step, _view.GetSlotsCount());
        }

        void IClearable.Clear()
        {
            _actionsProvider.ButtonClicked -= HandleButtonClicked;
        }

        private void HandleButtonClicked()
        {
        }
    }
}