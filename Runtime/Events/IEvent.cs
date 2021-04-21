using System;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public interface IInvokableEvent : IEvent
    {
        void Invoke();
    }

    public interface IInvokableEvent<T> : IEvent<T>
    {
        void Invoke(T context);
    }

    public interface IEvent<out T> : IEvent
    {
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }

    public interface IEvent
    {
        void Subscribe(Action action);
        void Unsubscribe(Action action);
    }

    public static class EventExtensions
    {
        public static CustomYieldInstruction Await(this IEvent @event)
        {
            return new EventAwaiter(@event.Subscribe, @event.Unsubscribe);
        } 
        
        public static CustomYieldInstruction Await<T>(this IEvent<T> @event)
        {
            return new EventAwaiter<T>(@event.Subscribe, @event.Unsubscribe);
        }
    }
}