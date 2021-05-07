using System;
using Chinchillada;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Chinchillada.Timers
{
    /// <summary>
    /// Timer that has a randomized duration for each run.
    /// </summary>
    [Serializable]
    public class RandomTimer : ITimer
    {
        /// <summary>
        /// Range of possible random durations.
        /// </summary>
        [FormerlySerializedAs("_durationRange")] [SerializeField]
        private Vector2 durationRange = new Vector2(0, 1);

        /// <summary>
        /// Event invoked when the timer is finished running.
        /// </summary>
        [FormerlySerializedAs("_finished")] [SerializeField]
        private UnityEvent finished;

        /// <summary>
        /// The current timer.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Range of possible random durations.
        /// </summary>
        public Vector2 DurationRange
        {
            get => this.durationRange;
            set => this.durationRange = value;
        }

        /// <inheritdoc />
        public float Duration => this.timer?.Duration ?? 0;

        /// <inheritdoc />
        public UnityEvent Finished => this.finished;

        /// <inheritdoc />
        public bool IsRunning => this.timer?.IsRunning ?? false;

        /// <summary>
        /// Starts the timer with a random duration.
        /// </summary>
        public void StartRandom()
        {
            var duration = this.durationRange.RandomInRange();
            this.timer = Timer.StartTimer(duration, this.Finish);
        }

        /// <summary>
        /// Restarts the timer with a new random duration.
        /// </summary>
        public void RestartRandom()
        {
            this.timer.Stop();
            this.StartRandom();
        }

        /// <summary>
        /// Starts the timer.
        /// If the timer was paused, continues. Else, will generate random duration.
        /// </summary>
        public void Start()
        {
            if (this.timer != null)
                this.timer.Start();
            else
                this.StartRandom();
        }

        /// <inheritdoc />
        public void Pause() => this.timer.Pause();

        /// <inheritdoc />
        public void Restart() => this.timer.Restart();

        /// <inheritdoc />
        public void Finish()
        {
            this.timer = null;
            this.Finished?.Invoke();
        }

        /// <inheritdoc />
        public void Stop()
        {
            this.timer.Stop();
            this.timer = null;
        }
    }
}