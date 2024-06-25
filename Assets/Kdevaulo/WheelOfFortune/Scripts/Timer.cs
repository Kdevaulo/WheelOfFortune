using System;
using System.Threading;

using Cysharp.Threading.Tasks;

namespace Kdevaulo.WheelOfFortune
{
    /// <summary>
    /// Timer based on UniTask WaitForSeconds
    /// </summary>
    public class Timer
    {
        public event Action Ticked = delegate { };
        public event Action Finished = delegate { };

        private CancellationTokenSource _cts;

        public void Start(float timeInSeconds, float tickDelayInSeconds)
        {
            RefreshToken();

            HandleTimerAsync(timeInSeconds, tickDelayInSeconds, _cts.Token).Forget();
        }

        private void RefreshToken()
        {
            if (_cts is { IsCancellationRequested: false })
            {
                _cts.Cancel();
                _cts.Dispose();
            }

            _cts = new CancellationTokenSource();
        }

        private async UniTask HandleTimerAsync(float timeInSeconds, float tickDelayInSeconds, CancellationToken token)
        {
            while (timeInSeconds >= tickDelayInSeconds)
            {
                await UniTask.WaitForSeconds(tickDelayInSeconds, cancellationToken: token);

                Ticked.Invoke();

                timeInSeconds -= tickDelayInSeconds;
            }

            if (timeInSeconds > 0)
            {
                await UniTask.WaitForSeconds(timeInSeconds, cancellationToken: token);
            }

            Finished.Invoke();
        }
    }
}