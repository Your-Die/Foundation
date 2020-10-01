namespace Chinchillada.Foundation.Events
{
    public class DestroyEvent : SimpleEvent
    {
        private void OnDestroy() => this.Event.Invoke();
    }
}