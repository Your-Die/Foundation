namespace Mutiny.Thesis.UI
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class ClickEvent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool checkEventUse;
        [SerializeField] private bool useEvent;

        [SerializeField] private UnityEvent clickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.checkEventUse && eventData.used)
                return;

            this.clickEvent.Invoke();

            if (this.useEvent)
                eventData.Use();
        }
    }
}