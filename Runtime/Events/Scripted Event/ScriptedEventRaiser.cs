using UnityEngine;

namespace Chinchillada
{
    using Sirenix.OdinInspector;

    public class ScriptedEventRaiser : MonoBehaviour
    {
        [SerializeField] private ScriptedEvent @event;

        [Button]
        public void Raise() => this.@event.Invoke();
    }
}
