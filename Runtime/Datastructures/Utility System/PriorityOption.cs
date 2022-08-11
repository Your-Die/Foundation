namespace Chinchillada
{
    public interface IPriorityOption<T>
    {
        object ID      { get; }
        int    Priority { get; }
        T      Content { get; }
    }

    public class PriorityOption<T> : IPriorityOption<T>
    {
        public object ID { get; }
        public int Priority { get; }
        public T Content { get; }

        public PriorityOption(object id, T content, int utility)
        {
            this.ID = id;
            this.Content = content;
            this.Priority = utility;
        }
    }
}