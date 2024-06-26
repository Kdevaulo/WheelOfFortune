using System;

namespace Kdevaulo.WheelOfFortune
{
    public class StateHandler
    {
        private readonly BaseStateHandler[] _stateHandlers;
        private readonly IUserActionsProvider _userActionsProvider;

        public StateHandler(IUserActionsProvider userActionsProvider, params BaseStateHandler[] stateHandlers)
        {
            _userActionsProvider = userActionsProvider;
            _stateHandlers = stateHandlers;

            _userActionsProvider.ButtonClicked += HandleButtonClick;

            Array.ForEach(_stateHandlers, x => x.SwitchStateCalled += HandleStateSwitch);
        }

        public void Start()
        {
            HandleStateSwitch(State.Cooldown);
        }

        private void HandleButtonClick()
        {
            HandleStateSwitch(State.ChoosingAward);
        }

        private void HandleStateSwitch(State state)
        {
            switch (state)
            {
                case State.Active:
                    Array.ForEach(_stateHandlers, x => x.HandleActiveState());
                    break;

                case State.Cooldown:
                    Array.ForEach(_stateHandlers, x => x.HandleCooldownState());
                    break;

                case State.ChoosingAward:
                    Array.ForEach(_stateHandlers, x => x.HandleChoosingRewardState());
                    break;
            }
        }
    }
}