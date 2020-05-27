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
}