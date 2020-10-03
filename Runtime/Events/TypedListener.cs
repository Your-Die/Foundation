using System;

namespace Chinchillada.Foundation
{
    public class Listener
    {
        private readonly Action action;

        public Listener(Action action)
        {
            this.action = action;
        }

        public void Invoke() => this.action.Invoke();
    }

    public interface IListener<T>
    {
        void Invoke(T context);
    }

    public class Listener<T> : IListener<T>
    {
        private readonly Action<T> action;

        public Listener(Action<T> action)
        {
            this.action = action;
        }

        public void Invoke(T context) => this.action.Invoke(context);
    }
    
    public class TypedListener<T> : IListener<T>
    {
        private readonly Action action;

        public TypedListener(Action action)
        {
            this.action = action;
        }

        public void Invoke(T _)
        {
            this.action.Invoke();
        }
    }
}