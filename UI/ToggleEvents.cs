using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mutiny.Foundation.UI
{
    public class ToggleEvents : ChinchilladaBehaviour
    {
        [SerializeField, Required, FindComponent(SearchStrategy.InChildren)]
        private Toggle toggle;

        [SerializeField] private bool fireOnStart;

        [SerializeField] private UnityEvent onTrue;

        [SerializeField] private UnityEvent onFalse;


        [Button]
        public void InvokeCurrent()
        {
            var current = this.toggle.isOn;
            this.InvokeEvent(current);
        }

        private void InvokeEvent(bool value)
        {
            var @event = value ? this.onTrue : this.onFalse;
            @event.Invoke();
        }

        private void Start()
        {
            if (this.fireOnStart)
                this.InvokeCurrent();
        }

        private void OnEnable() => this.toggle.onValueChanged.AddListener(this.InvokeEvent);

        private void OnDisable() => this.toggle.onValueChanged.RemoveListener(this.InvokeEvent);
    }
}