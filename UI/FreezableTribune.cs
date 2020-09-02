using UnityEngine;

namespace Chinchillada.Foundation.UI
{
    public abstract class FreezableTribune<T> : ChinchilladaBehaviour, IPerformer<T>, IFreezableTribune
    {
        [SerializeField] private int freezePriority = 2;

        [SerializeField] private LogHandler tribuneLogHandler;

        private T currentContent;

        public bool IsSummoned { get; private set; }

        public Tribune<T> Tribune { get; private set; }

        public void Summon(object summoner, int priority, T content)
        {
            this.Tribune.JoinAudience(summoner, priority, content);
        }

        public void Unsummon(object summoner)
        {
            this.Tribune.LeaveAudience(summoner);
        }

        public virtual void ForceHide() => this.Tribune.Clear();

        public virtual void Freeze()
        {
            if (this.IsSummoned == false)
                return;

            this.Tribune.JoinAudience(this, this.freezePriority, this.currentContent);
        }


        public virtual void Unfreeze()
        {
            this.Tribune.LeaveAudience(this);
        }

        protected override void Awake()
        {
            base.Awake();
            this.Tribune = new Tribune<T>(this)
            {
                Logger = this.tribuneLogHandler
            };
        }

        protected abstract void Show(T content);

        protected abstract void Hide();

        private void ShowInternal(T content)
        {
            this.IsSummoned = true;
            this.currentContent = content;
            
            this.Show(content);
        }

        private void HideInternal()
        {
            this.IsSummoned = false;
            this.currentContent = default;
            
            this.Hide();
        }
        
        void IPerformer<T>.PerformRequest(T request)
        {
            if (request == null)
                this.HideInternal();
            else
                this.ShowInternal(request);
        }

        void IPerformer<T>.StopPerformance() => this.Hide();
    }
}