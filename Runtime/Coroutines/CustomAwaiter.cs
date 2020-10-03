using System;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public class CustomAwaiter : CustomYieldInstruction
    {
        private readonly Func<bool> shouldWaitFunction;
        
        public override bool keepWaiting => this.shouldWaitFunction.Invoke();

        public CustomAwaiter(Func<bool> shouldWaitFunction)
        {
            this.shouldWaitFunction = shouldWaitFunction;
        }
    }
}