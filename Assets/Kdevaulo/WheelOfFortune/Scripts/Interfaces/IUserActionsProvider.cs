using System;

namespace Kdevaulo.WheelOfFortune
{
    public interface IUserActionsProvider
    {
        event Action ButtonClicked;
    }
}