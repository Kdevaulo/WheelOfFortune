using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Kdevaulo.WheelOfFortune.WheelGenerationBehaviour
{
    [AddComponentMenu(nameof(WheelView) + " in " + nameof(WheelGenerationBehaviour))]
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _rotationContainer;

        [SerializeField] private Image _rewardImage;

        private SlotView[] _slotViews;

        private int _slotsCount;

        public SlotView[] GetSlotViews()
        {
            if (_slotsCount == 0)
            {
                _slotViews = _slotsContainer.GetComponentsInChildren<SlotView>();
                _slotsCount = _slotViews.Length;
            }

            Assert.IsFalse(_slotsCount == 0);

            return _slotViews;
        }

        public void SetValues(int[] values)
        {
            Assert.IsTrue(values.Length <= _slotsCount);

            for (int i = 0; i < _slotsCount; i++)
            {
                _slotViews[i].SetText(values[i].ToString());
            }
        }

        public void SetRewardSprite(Sprite sprite)
        {
            _rewardImage.sprite = sprite;
        }

        public async UniTask RotateAsync(float zRotation, float duration, AnimationCurve curve, CancellationToken token)
        {
            float currentRotation = ClampRotation(_rotationContainer.rotation.eulerAngles.z);
            float offsetToZero = 360 - currentRotation;
            float targetRotation = ClampRotation(zRotation);

            await _rotationContainer
                .DORotate(new Vector3(0, 0, offsetToZero + targetRotation + 720), duration, RotateMode.LocalAxisAdd)
                .SetEase(curve)
                .AwaitForComplete(cancellationToken: token);
        }

        private float ClampRotation(float rotation)
        {
            return rotation >= 0 ? rotation : 360 + rotation;
        }
    }
}