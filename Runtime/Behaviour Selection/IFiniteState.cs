namespace Chinchillada.Behavior
{
    public interface IFiniteState
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
        public static bool TryEnter(this IFiniteState finiteState)
        {
            if (finiteState.IsActive)
                return false;

            finiteState.Enter();
            return true;
        }

        public static bool TryExit(this IFiniteState finiteState)
        {
            if (finiteState.IsActive == false)
                return false;

            finiteState.Exit();
            return true;
        }
    }
}