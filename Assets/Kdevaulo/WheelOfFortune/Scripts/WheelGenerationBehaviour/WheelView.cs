using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Kdevaulo.WheelOfFortune.WheelGenerationBehaviour
{
    [AddComponentMenu(nameof(WheelView) + " in " + nameof(WheelGenerationBehaviour))]
    public class WheelView : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnableRewardsContainer;

        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _rotationContainer;

        [Header("RewardText")]
        [SerializeField] private GameObject _rewardTextContainer;

        [SerializeField] private TextMeshProUGUI _rewardTextHolder;
        [SerializeField] private TextMeshProUGUI _rewardTextOutlineHolder;

        [Header("RewardImage")]
        [SerializeField] private GameObject _rewardImageContainer;
        [SerializeField] private Image _rewardImage;

        private int _slotsCount;

        private SlotView[] _slotViews;

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

        public void EnableRewardImage()
        {
            _rewardImageContainer.SetActive(true);
        }

        public void DisableRewardImage()
        {
            _rewardImageContainer.SetActive(false);
        }

        public void EnableRewardText()
        {
            _rewardTextContainer.SetActive(true);
        }

        public void DisableRewardText()
        {
            _rewardTextContainer.SetActive(false);
        }

        public void SetRewardText(string text)
        {
            _rewardTextHolder.text = text;
            _rewardTextOutlineHolder.text = text;
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

        public Vector2 GetFinishPosition()
        {
            return SpawnableRewardsContainer.position;
        }
    }
}