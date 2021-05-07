namespace Chinchillada
{
    public class And : IOperator<bool>
    {
        public bool Execute(bool left, bool right) => left && right;
    }
    public class Or : IOperator<bool>
    {
        public bool Execute(bool left, bool right) => left || right;
    }
}