using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Timers
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> wrapper for a <see cref="Timer"/>.
    /// </summary>
    public class TimerComponent : MonoBehaviour, ITimer
    {
        /// <summary>
        /// The timer.
        /// </summary>
        [SerializeField] private Timer _timer = new Timer();

        ///<inheritdoc />
        public float Duration => _timer.Duration;

        ///<inheritdoc />
        public UnityEvent Finished => _timer.Finished;

        ///<inheritdoc />
        public bool IsRunning => _timer.IsRunning;

        ///<inheritdoc />
        public void Start() => _timer.Start();

        ///<inheritdoc />
        public void Pause() => _timer.Pause();

        ///<inheritdoc />
        public void Restart() => _timer.Restart();

        ///<inheritdoc />
        public void Finish() => _timer.Finish();

        ///<inheritdoc />
        public void Stop() => _timer.Stop();
    }
}
