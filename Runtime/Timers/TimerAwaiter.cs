using UnityEngine;

namespace Chinchillada.Timers
{
    public class TimerAwaiter : CustomYieldInstruction
    {
        private readonly ITimer timer;
        
        public override bool keepWaiting => this.timer.IsRunning;
        
        public TimerAwaiter(ITimer timer)
        {
            this.timer = timer;
        } 
    }
}