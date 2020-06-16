﻿using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Mutiny.Foundation
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