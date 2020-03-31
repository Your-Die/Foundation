namespace Robots
{
    public class AudienceMember<T>
    {
        public object Key { get; }
        public int Priority { get; }
        public T Request { get; }

        public AudienceMember(object key, int priority, T request)
        {
            this.Key = key;
            this.Priority = priority;
            this.Request = request;
        }
    }
}