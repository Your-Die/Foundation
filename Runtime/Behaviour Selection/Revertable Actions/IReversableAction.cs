namespace Chinchillada.Behavior
{
    public interface IRevertableAction : IAction
    {
        void Revert();
    }
}