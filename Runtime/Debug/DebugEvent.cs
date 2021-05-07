using Chinchillada;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada
{
    public class DebugEvent : ChinchilladaBehaviour
    {
        [SerializeField] private UnityEvent @event;

        [Button]
        public void Invoke()
        {
            this.@event.Invoke();
        }
    }
}