﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Kdevaulo.WheelOfFortune.WheelBehaviour
{
    [AddComponentMenu(nameof(WheelView) + " in " + nameof(WheelBehaviour))]
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Image _rewardImage;

        private SlotView[] _slotViews;

        private int _slotsCount;

        public int GetSlotsCount()
        {
            _slotViews = _slotsContainer.GetComponentsInChildren<SlotView>();

            _slotsCount = _slotViews.Length;
            Assert.IsFalse(_slotsCount == 0);

            return _slotsCount;
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
    }
}