namespace Robots
{
    using System;

    public interface IPerformer<T>
    {
        Tribune<T> Tribune { get; }
        
        void PerformRequest(T request);

        void StopPerformance();
    }
}