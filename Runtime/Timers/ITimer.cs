using UnityEngine.Events;

namespace Chinchillada.Timers
{
    using BombWatch;

    public interface ITimer : IHasDuration
    {
        UnityEvent Finished { get; }

        /// <summary>
        /// Whether the timer is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Starts running the timer.
        /// </summary>
        void Start();

        /// <summary>
        /// Pause the timer.
        /// </summary>
        void Pause();

        /// <summary>
        /// Restart the timer.
        /// </summary>
        void Restart();

        /// <summary>
        /// Manually finish the timer, making it stop and invoke <see cref="Timer.Finished"/>.
        /// </summary>
        void Finish();

        /// <summary>
        /// Manually stop the timer without invoking <see cref="Timer.Finished"/>.
        /// </summary>
        void Stop();
    }
}