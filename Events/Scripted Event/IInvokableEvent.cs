using System;

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
}