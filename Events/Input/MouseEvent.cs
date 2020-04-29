using System;
using UnityEngine;

namespace Mutiny.Foundation.Events
{
    public class MouseEvent : SimpleEvent
    {
        [SerializeField] private int mouseButton = 0;

        private void Update()
        {
            if (Input.GetMouseButtonDown(this.mouseButton)) 
                this.Event.Invoke();
        }
    }
}