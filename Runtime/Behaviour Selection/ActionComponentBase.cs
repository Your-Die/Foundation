namespace Chinchillada.Behavior
{
    public abstract class ActionComponentBase : AutoRefBehaviour, IAction
    {
        public abstract void Trigger();
    }
}