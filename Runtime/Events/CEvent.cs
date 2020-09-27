using System;

namespace Chinchillada.Foundation
{
    public class CEvent : IInvokableEvent
    {
        private event Action Event;
        
        public void Subscribe(Action action) => this.Event += action;

        public void Unsubscribe(Action action) => this.Event -= action;

        public void Invoke() => this.Event?.Invoke();
    }
}