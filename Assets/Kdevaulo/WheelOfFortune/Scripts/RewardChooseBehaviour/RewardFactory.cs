using UnityEngine;

namespace Kdevaulo.WheelOfFortune.RewardChooseBehaviour
{
    public class RewardFactory
    {
        private readonly Transform _parent;
        private readonly RewardView _view;

        private Sprite _sprite;

        public RewardFactory(RewardView view, Transform parent)
        {
            _view = view;
            _parent = parent;
        }

        public RewardView Create()
        {
            var view = Object.Instantiate(_view, _parent);
            view.SetSprite(_sprite);

            return view;
        }

        public void Initialize(Sprite sprite)
        {
            _sprite = sprite;
        }
    }
}