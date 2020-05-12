using UnityEngine;
using UnityEngine.Serialization;

namespace Chinchillada.Events
{
    public class ScriptedEventRaiser : MonoBehaviour
    {
        [FormerlySerializedAs("_event")] [SerializeField] private ScriptedEvent @event;

        [SerializeField] private bool log = true;
        
        public void Raise()
        {
            if (this.log)
            {
                Debug.Log("Raisin event: " + this.@event);
            }
            
            this.@event.Raise();
        }
    }
}
