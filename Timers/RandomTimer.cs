using System;
using Chinchillada.Utilities;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private Vector2 _durationRange = new Vector2(0, 1);

        /// <summary>
        /// Event invoked when the timer is finished running.
        /// </summary>
        [SerializeField] private UnityEvent _finished;

        /// <summary>
        /// The current timer.
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Range of possible random durations.
        /// </summary>
        public Vector2 DurationRange
        {
            get => _durationRange;
            set => _durationRange = value;
        }

        /// <inheritdoc />
        public float Duration => _timer?.Duration ?? 0;

        /// <inheritdoc />
        public UnityEvent Finished => _finished;

        /// <inheritdoc />
        public bool IsRunning => _timer?.IsRunning ?? false;

        /// <summary>
        /// Starts the timer with a random duration.
        /// </summary>
        public void StartRandom()
        {
            float duration = _durationRange.RandomInRange();
            _timer = Timer.StartTimer(duration, Finish);
        }

        /// <summary>
        /// Restarts the timer with a new random duration.
        /// </summary>
        public void RestartRandom()
        {
            _timer.Stop();
            StartRandom();
        }

        /// <summary>
        /// Starts the timer.
        /// If the timer was paused, continues. Else, will generate random duration.
        /// </summary>
        public void Start()
        {
            if (_timer != null)
                _timer.Start();
            else
            {
                StartRandom();
            }
        }

        /// <inheritdoc />
        public void Pause()
        {
            _timer.Pause();
        }

        /// <inheritdoc />
        public void Restart()
        {
            _timer.Restart();
        }

        /// <inheritdoc />
        public void Finish()
        {
            _timer = null;
            Finished?.Invoke();
        }

        /// <inheritdoc />
        public void Stop()
        {
            _timer.Stop();
            _timer = null;
        }
    }
}
