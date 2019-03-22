using System;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [CreateAssetMenu(menuName = "Chinchillada/Event", fileName = "Event")]
    public class ScriptedEvent : ScriptableObject
    {
        public event Action Happened;

        public void Raise()
        {
            Happened?.Invoke();
        }
    }
}