using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;

namespace Mutiny.Foundation.Common
{
    public class Garbage : SingletonBehaviour<Garbage>
    {
        private List<Action> taskQueue = new List<Action>();
        
        public static void Register<T>(ICollection<T> collection, T garbage)
        {
            void Action() => collection.Remove(garbage);
            
            Instance.Register(Action);
        }

        public static void Register<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            void Action() => dictionary.Remove(key);
            Instance.Register(Action);
        }
       
        public void CollectGarbage()
        {
            foreach (var collection in this.taskQueue) 
                collection.Invoke();
            
            this.taskQueue.Clear();
        }
        

        private void Register(Action action) => this.taskQueue.Add(action);

        private void LateUpdate()
        {
            if (this.taskQueue.Any()) 
                this.CollectGarbage();
        }
    }
}