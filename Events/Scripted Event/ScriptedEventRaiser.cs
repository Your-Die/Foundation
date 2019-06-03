using UnityEngine;

namespace Chinchillada.Events
{
    public class ScriptedEventRaiser : MonoBehaviour
    {
        [SerializeField] private ScriptedEvent _event;

        public void Raise() => _event.Raise();
    }
}
