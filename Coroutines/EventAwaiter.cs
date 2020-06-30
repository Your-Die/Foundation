using System;
using Mutiny.Foundation.Common;
using UnityEngine;

namespace Mutiny.Foundation.Coroutines
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
}