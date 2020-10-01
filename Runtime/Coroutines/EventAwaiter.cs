using System;
using Chinchillada.Foundation.Common;
using UnityEngine;

namespace Chinchillada.Foundation.Coroutines
{
    public class EventAwaiter : CustomYieldInstruction
    {
        private bool isEventTriggered;

        public override bool keepWaiting => !this.isEventTriggered;


        public EventAwaiter(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            OneTimeSubscriber.CreateAndSubscribe(this.OnEventTriggered, subscribe, unsubscribe);
        }

        private void OnEventTriggered() => this.isEventTriggered = true;
    }

    public class EventAwaiter<T> : CustomYieldInstruction
    {
        private bool isEventTriggered;

        public override bool keepWaiting => !this.isEventTriggered;

        public T Result { get; private set; }

        public EventAwaiter(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            OneTimeSubscriber<T>.CreateAndSubscribe(this.OnEventTriggered, subscribe, unsubscribe);
        }

        private void OnEventTriggered(T result)
        {
            this.Result = result;
            this.isEventTriggered = true;
        }
    }
}