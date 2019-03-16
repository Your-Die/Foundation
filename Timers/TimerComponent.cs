using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Timers
{
    public class TimerComponent : MonoBehaviour, ITimer
    {
        [SerializeField] private Timer _timer = new Timer();

        public float Duration => _timer.Duration;
        public UnityEvent Finished => _timer.Finished;
        public bool IsRunning => _timer.IsRunning;

        public void Start() => _timer.Start();

        public void Pause() => _timer.Pause();

        public void Restart() => _timer.Restart();

        public void Finish() => _timer.Finish();

        public void Stop() => _timer.Stop();
    }
}
