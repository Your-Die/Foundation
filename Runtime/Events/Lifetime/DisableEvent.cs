namespace Chinchillada.Events
{
    public class DisableEvent : SimpleEvent
    {
        private void OnDisable() => this.Event.Invoke();
    }
}