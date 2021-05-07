namespace Chinchillada
{
    public abstract class SubscriberBehaviour : ChinchilladaBehaviour
    {
        public bool IsSubscribed { get; private set; }

        public void Subscribe()
        {
            if (this.IsSubscribed)
                return;

            this.ActivateSubscriptions();

            this.IsSubscribed = true;
        }

        public void Unsubscribe()
        {
            if (!this.IsSubscribed)
                return;

            this.DeactivateSubscriptions();

            this.IsSubscribed = false;
        }

        protected abstract void ActivateSubscriptions();
        protected abstract void DeactivateSubscriptions();
    }
}