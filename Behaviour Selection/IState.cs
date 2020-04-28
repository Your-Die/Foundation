namespace Mutiny.Foundation.SSM
{
    public interface IState : ITickable
    {
        bool IsActive { get; }

        void Enter();
        void Exit();
    }

    public static class StateExtensions
    {
        public static bool TryEnter(this IState state)
        {
            if (state.IsActive)
                return false;

            state.Enter();
            return true;
        }

        public static bool TryExit(this IState state)
        {
            if (state.IsActive == false)
                return false;
            
            state.Exit();
            return true;
        }
    }
}