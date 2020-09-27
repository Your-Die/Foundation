namespace Mutiny.Foundation.Events
{
    public class StartEvent : SimpleEvent
    {

        private void Start() => this.Event.Invoke();
    }
}