namespace Chinchillada.Foundation
{
    public interface IPerformer<in T>
    {
        void PerformRequest(T request);

        void StopPerformance();
    }
}