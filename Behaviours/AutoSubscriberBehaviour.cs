using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public abstract class AutoSubscriberBehaviour : SubscriberBehaviour
    {
        private SubscriptionStrategy strategy;

        protected virtual SubscriptionStrategy Strategy => this.strategy;

        protected virtual void OnEnable()
        {
            if (this.Strategy == SubscriptionStrategy.EnableDisable)
                this.Subscribe();
        }

        protected virtual void OnDisable()
        {
            if (this.Strategy == SubscriptionStrategy.EnableDisable)
                this.Unsubscribe();
        }

        protected override void Awake()
        {
            base.Awake();
            
            if (this.Strategy == SubscriptionStrategy.AwakeDestroy)
                this.Subscribe();
        }

        protected virtual void Start()
        {
            if (this.Strategy == SubscriptionStrategy.StartDestroy) 
                this.Subscribe();
        }

        protected virtual void OnDestroy()
        {
            if (this.Strategy == SubscriptionStrategy.AwakeDestroy || 
                this.Strategy == SubscriptionStrategy.StartDestroy)
                this.Unsubscribe();
        }
    }

    public enum SubscriptionStrategy
    {
        Manual,
        EnableDisable,
        AwakeDestroy,
        StartDestroy
    }
}