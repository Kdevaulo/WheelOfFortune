using UnityEngine;

namespace Kdevaulo.WheelOfFortune.RewardChooseBehaviour
{
    public class RewardFactory
    {
        private readonly RewardView _view;
        private readonly Transform _parent;

        private Sprite _sprite;

        public RewardFactory(RewardView view, Transform parent)
        {
            _view = view;
            _parent = parent;
        }

        public void Initialize(Sprite sprite)
        {
            _sprite = sprite;
        }

        public RewardView Create()
        {
            var view = Object.Instantiate(_view, _parent);
            view.SetSprite(_sprite);

            return view;
        }
    }
}