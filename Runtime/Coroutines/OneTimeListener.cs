namespace Coroutines
{
    using UnityEngine.Events;

    public class OneTimeListener
    {
        private readonly UnityEvent @event;
        private readonly UnityAction action;

        public static OneTimeListener Create(UnityEvent @event, UnityAction action)
        {
            var subscriber = new OneTimeListener(@event, action);
            subscriber.Subscribe();

            return subscriber;
        }
        
        public OneTimeListener(UnityEvent @event, UnityAction action)
        {
            this.@event = @event;
            this.action = action;
        }

        public void Subscribe()
        {
            this.@event.AddListener(this.OnEventInvoked);
        }

        public void Unsubscribe()
        {
            this.@event.RemoveListener(this.OnEventInvoked);
        }

        private void OnEventInvoked()
        {
            this.Unsubscribe();
            this.action.Invoke();
        }
    }
}