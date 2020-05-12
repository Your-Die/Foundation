namespace Chinchillada.Foundation
{
    public interface IPerformer<T>
    {
        Tribune<T> Tribune { get; }
        
        void PerformRequest(T request);

        void StopPerformance();
    }

    public static class PerformerMethods
    {
        public static void JoinAudience<T>(this IPerformer<T> performer, object context, int priority, T request)
        {
            var audienceMember= new AudienceMember<T>(context, priority, request);
            performer.Tribune.JoinAudience(audienceMember);
        }

        public static void LeaveAudience<T>(this IPerformer<T> performer, object context)
        {
            performer.Tribune.LeaveAudience(context);
        }
    }
}