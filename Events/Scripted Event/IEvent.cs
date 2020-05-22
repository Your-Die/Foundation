using System;

namespace Chinchillada.Foundation
{
    public interface IEvent
    {
        void Invoke();
        
        void Subscribe(Action action);
        void Unsubscribe(Action action);
    }
    
    public interface IEvent<T>
    {
        void Invoke(T context);
        
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }
}