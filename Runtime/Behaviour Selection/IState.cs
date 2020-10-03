namespace Chinchillada.Foundation.States
{
    public interface IState
    {
        bool IsActive { get; }
        void Enter();
        void Exit();
    }

    public interface IState<T>
    {
        bool IsActive { get; }
        void Enter(T context);
        void Exit(T context);
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