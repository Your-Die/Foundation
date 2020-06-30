using System;
using UnityEngine;

namespace Mutiny.Foundation.Coroutines
{
    public class EventPredicator<T> : CustomYieldInstruction
    {
        private bool isEventTriggered;

        public override bool keepWaiting => !this.isEventTriggered;


        public EventPredicator(Func<T, bool> predicate, Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            PredicateSubscriber<T>.CreateAndSubscribe(predicate, this.OnSatisfied, subscribe, unsubscribe);
        }
        
        private void OnSatisfied(T obj)
        {
            this.isEventTriggered = true;
        }
    }
}