using System;

namespace Mutiny.Foundation.Common
{
    public class OneTimeSubscriber
    {
        private readonly Action onEvent;
        
        private readonly Action<Action> subscribeAction;
        private readonly Action<Action> unsubscribeAction;

        public OneTimeSubscriber(Action onEvent, Action<Action> subscribeAction, Action<Action> unsubscribeAction)
        {
            this.onEvent = onEvent;
            
            this.subscribeAction = subscribeAction;
            this.unsubscribeAction = unsubscribeAction;
        }

        public void Subscribe() => this.subscribeAction.Invoke(this.InvokeOnce);

        public void UnSubscribe() => this.unsubscribeAction.Invoke(this.InvokeOnce);

        private void InvokeOnce()
        {
            this.UnSubscribe();
            this.onEvent.Invoke();
        }

        public static OneTimeSubscriber CreateAndSubscribe(Action onEvent, Action<Action> subscribe,
            Action<Action> unsubscribe)
        {
            var subscriber = new OneTimeSubscriber(onEvent, subscribe, unsubscribe);
            subscriber.Subscribe();

            return subscriber;
        }
    }

    public class OneTimeSubscriber<T>
    {
        private readonly Action<T> onEvent;
        
        private readonly Action<Action<T>> subscribeAction;
        private readonly Action<Action<T>> unsubscribeAction;

        public OneTimeSubscriber(Action<T> onEvent, Action<Action<T>> subscribeAction, Action<Action<T>> unsubscribeAction)
        {
            this.onEvent = onEvent;
            
            this.subscribeAction = subscribeAction;
            this.unsubscribeAction = unsubscribeAction;
        }

        public void Subscribe() => this.subscribeAction.Invoke(this.InvokeOnce);

        public void UnSubscribe() => this.unsubscribeAction.Invoke(this.InvokeOnce);

        private void InvokeOnce(T parameter)
        {
            this.UnSubscribe();
            this.onEvent.Invoke(parameter);
        }

        public static OneTimeSubscriber<T> CreateAndSubscribe(Action<T> onEvent, Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe)
        {
            var subscriber = new OneTimeSubscriber<T>(onEvent, subscribe, unsubscribe);
            subscriber.Subscribe();

            return subscriber;
        }
    }
}