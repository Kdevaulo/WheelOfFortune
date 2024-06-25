using System;

using UnityEngine;

namespace Kdevaulo.WheelOfFortune
{
    [Serializable]
    public class Reward
    {
        // note: here we can use a more reliable identifier than a string
        public string Id;
        public Sprite Sprite;
    }
}