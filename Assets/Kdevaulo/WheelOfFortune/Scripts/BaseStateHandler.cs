using System;

namespace Kdevaulo.WheelOfFortune
{
    public abstract class BaseStateHandler
    {
        public event Action<State> SwitchStateCalled = delegate { };

        public virtual void HandleActiveState()
        {
        }

        public virtual void HandleChoosingRewardState()
        {
        }

        public virtual void HandleCooldownState()
        {
        }

        protected void SwitchState(State state)
        {
            SwitchStateCalled.Invoke(state);
        }
    }
}