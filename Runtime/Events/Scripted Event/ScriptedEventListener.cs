using UnityEngine.Events;

namespace Chinchillada
{
    /// <summary>
    /// Component that propagates <see cref="ScriptedEvent"/> to <see cref="UnityEvent"/>.
    /// </summary>
    public class ScriptedEventListener : EventListenerBase
    {
        /// <summary>
        /// The event invoked when the <see cref="EventListenerBase.Event"/> is raised.
        /// </summary>
        public UnityEvent response;

        /// <summary>
        /// Called when the <see cref="EventListenerBase.Event"/> happened.
        /// Invokes the <see cref="response"/>.
        /// </summary>
        protected override void OnEventHappened() => this.response?.Invoke();
    }
}