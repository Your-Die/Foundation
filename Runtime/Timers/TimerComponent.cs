using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("_timer")] [SerializeField]
        private Timer timer = new Timer();

        ///<inheritdoc />
        public float Duration => this.timer.Duration;

        public float Remaining => this.timer.Remaining;

        ///<inheritdoc />
        public UnityEvent Finished => this.timer.Finished;

        ///<inheritdoc />
        public bool IsRunning => this.timer.IsRunning;

        ///<inheritdoc />
        public void Start() => this.timer.Start();

        ///<inheritdoc />
        public void Pause() => this.timer.Pause();

        ///<inheritdoc />
        public void Restart() => this.timer.Restart();

        ///<inheritdoc />
        public void Finish() => this.timer.Finish();

        ///<inheritdoc />
        public void Stop() => this.timer.Stop();
    }
}