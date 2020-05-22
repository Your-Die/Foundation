using System;

namespace Chinchillada.Foundation
{
    public class PoolList<T> : PoolListBase<T>
    {
        private readonly Func<T> factoryMethod;

        public PoolList(Func<T> factoryMethod)
        {
            this.factoryMethod = factoryMethod;
        }
        
        protected override T CreateNew() => this.factoryMethod.Invoke();
    }
}