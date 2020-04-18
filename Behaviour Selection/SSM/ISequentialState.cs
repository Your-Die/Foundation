namespace Mutiny.Foundation.SSM
{
    public interface ISequentialState : IState
    {
        bool ShouldTransitionForward();
        
        bool ShouldTransitionBackward();
    }
}