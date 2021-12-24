namespace Chinchillada
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;
    using UnityEngine.Events;

    public class EventListener : AutoRefBehaviour
    {
        [OdinSerialize, Required, FindComponent(SearchStrategy.InChildren)]
        private IEvent @event;

        [SerializeField] private UnityEvent response = new UnityEvent();

        public UnityEvent Response => this.response;

        public IEvent Event
        {
            set => this.@event = value;
        }

        private void OnEnable() => this.@event.Subscribe(this.OnEvent);

        private void OnDisable() => this.@event.Unsubscribe(this.OnEvent);

        private void OnEvent() => this.Response.Invoke();
    }
}