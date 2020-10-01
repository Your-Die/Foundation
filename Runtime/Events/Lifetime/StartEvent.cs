namespace Chinchillada.Foundation.Events
{
    public class StartEvent : SimpleEvent
    {

        private void Start() => this.Event.Invoke();
    }
}