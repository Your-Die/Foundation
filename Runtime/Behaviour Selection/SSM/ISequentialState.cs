namespace Chinchillada.Foundation.States
{
    public interface ISequentialState : IState, ITickable
    {
        bool ShouldTransitionForward();
        
        bool ShouldTransitionBackward();
    }
}