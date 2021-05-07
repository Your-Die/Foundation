namespace Chinchillada.Events
{
    public class EnableEvent : SimpleEvent
    {

        private void OnEnable() => this.Event.Invoke();
    }
}