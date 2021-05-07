namespace Chinchillada.Events
{
    public class StartEvent : SimpleEvent
    {

        private void Start() => this.Event.Invoke();
    }
}