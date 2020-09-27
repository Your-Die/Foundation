using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Chinchillada.Timers
{
    /// <summary>
    /// A timer that runs for the <see cref="Duration"/> before invoking <see cref="Finished"/>.
    /// </summary>
    [Serializable]
    public class Timer : ITimer
    {
        [FormerlySerializedAs("_duration")] [SerializeField]
        private float duration;

        [FormerlySerializedAs("_finished")] [SerializeField]
        private UnityEvent finished = new UnityEvent();

        /// <summary>
        /// Handle to the routine.
        /// </summary>
        private IEnumerator timerRoutine;

        /// <summary>
        /// Time the current timer run was started.
        /// </summary>
        private float startTime;

        /// <summary>
        /// The amount of time that this timer has spent running since the last <see cref="Stop"/>, <see cref="Finish"/> or <see cref="Restart"/>.
        /// </summary>
        private float elapsedTime;

        /// <summary>
        /// Duration of the timer.
        /// </summary>
        public float Duration => this.duration;

        public UnityEvent Finished => this.finished;

        /// <summary>
        /// Whether the timer is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        public float ElapsedTime => Time.time - this.startTime;

        public float FinishedPercentage => this.ElapsedTime / this.Duration;

        #region Constructors

        /// <summary>
        /// Construct a new timer.
        /// </summary>
        public Timer()
        {
        }

        /// <summary>
        /// Construct a new timer.
        /// </summary>
        /// <param name="duration">The amount of time the timer should run.</param>
        public Timer(float duration)
        {
            this.duration = duration;
            this.finished = new UnityEvent();
        }

        /// <summary>
        /// Construct a new timer.
        /// </summary>
        /// <param name="duration">The amount of time the timer should run.</param>
        /// <param name="onFinished">Action invoked when the timer is finished.</param>
        public Timer(float duration, UnityAction onFinished) : this(duration)
        {
            this.Finished.AddListener(onFinished);
        }

        /// <summary>
        /// Constructs a new timer and immediately starts it.
        /// </summary>
        /// <param name="duration">The amount of time the timer should run.</param>
        /// <param name="onFinished">Action invoked when the timer is finished.</param>
        /// <returns></returns>
        public static Timer StartTimer(float duration, UnityAction onFinished = null)
        {
            var timer = new Timer(duration);

            if (onFinished != null)
                timer.Finished.AddListener(onFinished);

            timer.Start();
            return timer;
        }

        #endregion

        /// <summary>
        /// Starts running the timer.
        /// </summary>
        public void Start()
        {
            if (this.IsRunning)
                return;

            this.startTime = Time.time;
            this.IsRunning = true;

            var remaining = this.Duration - this.elapsedTime;

            if (remaining > 0)
                this.timerRoutine = Timing.InvokeDelayed(this.Finish, remaining);
            else
                this.Finish();
        }

        /// <summary>
        /// Pause the timer.
        /// </summary>
        public void Pause()
        {
            if (!this.IsRunning)
                return;

            Timing.CancelInvocation(this.timerRoutine);
            this.IsRunning = false;

            var time = Time.time;
            var elapsed = time - this.startTime;

            this.elapsedTime += elapsed;
        }

        /// <summary>
        /// Restart the timer.
        /// </summary>
        public void Restart()
        {
            this.Stop();
            this.Start();
        }

        /// <summary>
        /// Manually finish the timer, making it stop and invoke <see cref="Finished"/>.
        /// </summary>
        public void Finish()
        {
            this.Stop();
            this.Finished?.Invoke();
        }

        /// <summary>
        /// Manually stop the timer without invoking <see cref="Finished"/>.
        /// </summary>
        public void Stop()
        {
            Timing.CancelInvocation(this.timerRoutine);
            this.IsRunning = false;
            this.elapsedTime = 0;
        }
    }
}