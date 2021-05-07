namespace Chinchillada
{
    public class PriorityOption<T>
    {
        public object ID { get; }
        public int Utility { get; }
        public T Content { get; }

        public PriorityOption(object id, T content, int utility)
        {
            this.ID = id;
            this.Content = content;
            this.Utility = utility;
        }
    }
}