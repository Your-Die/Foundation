namespace Chinchillada.Foundation.UI
{
    using Foundation;
    using JetBrains.Annotations;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// <remarks>
    /// Todo: Rewrite to use <see cref="FreezableTribune{T}"/>
    /// </remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PresenterPopup<T> : Popup, ITribunePresenter<T>
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

        public void Unsummon(object summoner)
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