namespace Mutiny.Foundation.Events
{
    public class EnableEvent : SimpleEvent
    {

        private void OnEnable() => this.Event.Invoke();
    }
}