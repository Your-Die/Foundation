using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
{
    public class UnityEventAwaiter : CustomYieldInstruction
    {
        private bool isEventTriggered;

        public override bool keepWaiting => !this.isEventTriggered;

        private readonly UnityEvent @event;
        
        public UnityEventAwaiter(UnityEvent @event)
        {
            this.@event = @event;
            @event.AddListener(this.OnEventTriggered);
        }

        private void OnEventTriggered()
        {
            this.@event.RemoveListener(this.OnEventTriggered);
            this.isEventTriggered = true;
        }
    }
}