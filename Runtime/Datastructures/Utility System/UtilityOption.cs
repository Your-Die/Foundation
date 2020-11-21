namespace Chinchillada.Foundation
{
    public class UtilityOption<T>
    {
        public object ID { get; }
        public int Utility { get; }
        public T Content { get; }

        public UtilityOption(object id, T content, int utility)
        {
            this.ID = id;
            this.Content = content;
            this.Utility = utility;
        }
    }
}