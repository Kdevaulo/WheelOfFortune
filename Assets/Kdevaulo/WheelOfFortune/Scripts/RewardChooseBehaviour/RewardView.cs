using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace Kdevaulo.WheelOfFortune.RewardChooseBehaviour
{
    [AddComponentMenu(nameof(RewardView) + " in " + nameof(RewardChooseBehaviour))]
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _movingContainer;

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetPosition(Vector2 position)
        {
            _movingContainer.position = position;
        }

        public async UniTask AppearAsync(float appearDuration, Vector2 targetPosition, CancellationToken token)
        {
            _movingContainer.localScale = Vector3.zero;

            var sequence = DOTween.Sequence();

            await sequence
                .Append(_movingContainer.DOScale(Vector3.one, appearDuration))
                .Join(_movingContainer.DOMove(targetPosition, appearDuration))
                .SetEase(Ease.InSine)
                .AwaitForComplete(cancellationToken: token);
        }

        public async UniTask MoveToFinishAsync(float delay, float moveDuration, Vector2 targetPosition,
            CancellationToken token)
        {
            await UniTask.WaitForSeconds(delay, cancellationToken: token);

            var sequence = DOTween.Sequence();

            await sequence
                .Append(_movingContainer.DOScale(Vector3.zero, moveDuration))
                .Join(_movingContainer.DOMove(targetPosition, moveDuration))
                .SetEase(Ease.OutSine)
                .AwaitForComplete(cancellationToken: token);
        }
    }
}