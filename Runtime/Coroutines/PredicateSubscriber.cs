using System;

namespace Chinchillada
{
    public class PredicateSubscriber<T>
    {
        private readonly Func<T, bool> predicate;
        private readonly Action<T> onSatisfied;
        private readonly Action<Action<T>> subscribe;
        private readonly Action<Action<T>> unsubscribe;

        public PredicateSubscriber(Func<T, bool> predicate, Action<T> onSatisfied, 
            Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.predicate = predicate;
            this.onSatisfied = onSatisfied;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }
        
        public static PredicateSubscriber<T> CreateAndSubscribe(Func<T, bool> predicate, Action<T> onSatisfied, 
            Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            var subscriber = new PredicateSubscriber<T>(predicate, onSatisfied, subscribe, unsubscribe);
            subscriber.Subscribe();

            return subscriber;
        }

        public void Subscribe() => this.subscribe.Invoke(this.HandleEvent);

        public void Unsubscribe() => this.unsubscribe.Invoke(this.HandleEvent);

        private void HandleEvent(T item)
        {
            if (!this.predicate.Invoke(item))
                return;
            
            this.Unsubscribe();
            this.onSatisfied.Invoke(item);
        }
    }
}