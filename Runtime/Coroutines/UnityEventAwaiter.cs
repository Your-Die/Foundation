﻿using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Foundation.Coroutines
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

    public static class UnityEventAwaitExtensions
    {
        public static UnityEventAwaiter Await(this UnityEvent @event) => new UnityEventAwaiter(@event);
    }
}