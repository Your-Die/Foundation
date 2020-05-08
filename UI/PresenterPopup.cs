namespace Mutiny.Thesis.UI
{
    using Chinchillada.Utilities;
    using JetBrains.Annotations;
    using Robots;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public abstract class PresenterPopup<T> : Popup, IPerformer<T>
    {
        [SerializeField] private int freezePriority = 2;

        [SerializeField] private LogHandler tribuneLogHandler;
        
        [SerializeField, FindComponent, Required]
        private IPresenter<T> presenter;

        private T currentContent;
        
        public Tribune<T> Tribune { get; private set; }

        public void Summon(object summoner, int priority, T content)
        {
            this.Tribune.JoinAudience(summoner, priority, content);
        }

        public override void Unsummon(object summoner)
        {
            this.Tribune.LeaveAudience(summoner);
        }

        [UsedImplicitly]
        public void ForceHide() => this.Tribune.Clear();

        public override void Freeze()
        {
            if (this.IsSummoned == false)
                return;

            this.Tribune.JoinAudience(this, this.freezePriority, this.currentContent);
            base.Freeze();
        }

        public override void Unfreeze()
        {
            this.Tribune.LeaveAudience(this);
            base.Unfreeze();
        }

        protected override void Awake()
        {
            base.Awake();
            this.Tribune = new Tribune<T>(this)
            {
                Logger = this.tribuneLogHandler
            };
        }

        private void ShowInternal(T content)
        {
            this.currentContent = content;
            this.presenter.Present(content);
            base.Summon();
        }

        private void HideInternal()
        {
            this.presenter.Hide();
            this.Hide();
        }

        void IPerformer<T>.PerformRequest(T request)
        {
            if (request == null)
                this.HideInternal();
            else
                this.ShowInternal(request);
        }

        void IPerformer<T>.StopPerformance() => this.HideInternal();
    }
}