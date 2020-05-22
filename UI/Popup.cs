namespace Chinchillada.Foundation.UI
{
    using Foundation;
    using UnityEngine;

    public class Popup : ChinchilladaBehaviour
    {
        [SerializeField] private Vector3 offset;

        [SerializeField] private bool followMouse;

        [SerializeField] private LogHandler summonLogHandler;

        private RectTransform hostRect;
        private Camera cam;

        private bool shouldFollow;

        public bool IsSummoned { get; private set; }

        public virtual void Freeze() => this.shouldFollow = false;

        public virtual void Unfreeze() => this.shouldFollow = this.followMouse;
        
        public void Summon()
        {
            this.cam = this.hostRect.GetCanvasCamera();
            this.gameObject.SetActive(true);

            this.shouldFollow = this.followMouse;

            this.summonLogHandler.Log($"{this.name} summoned.");

            this.UpdatePosition();
            this.IsSummoned = true;
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);

            this.summonLogHandler.Log($"{this.name} hidden.");

            this.IsSummoned = false;
        }

        private void LateUpdate()
        {
            if (this.shouldFollow)
                this.UpdatePosition();
        }

        private void UpdatePosition()
        {
            this.hostRect.ScreenPointToWorldPoint(this.cam, Input.mousePosition, out var point);
            this.transform.position = point + this.offset;
        }

        protected override void Awake()
        {
            base.Awake();
            this.gameObject.SetActive(false);

            this.hostRect = this.transform.parent as RectTransform;
        }
    }
}