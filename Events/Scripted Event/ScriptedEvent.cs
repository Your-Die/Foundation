using System;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [CreateAssetMenu(menuName = "Chinchillada/Scripted Event", fileName = "Event")]
    public class ScriptedEvent : ScriptableObject
    {
        public event Action Raised;

        public void Raise()
        {
            Raised?.Invoke();
        }
    }
}