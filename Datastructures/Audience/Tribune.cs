namespace Chinchillada.Foundation
{
    using System.Collections.Generic;
    using Utilities;

    public class Tribune<T> 
    {
        private readonly IPerformer<T> performer;
        private readonly Dictionary<object, AudienceMember<T>> audience = new Dictionary<object, AudienceMember<T>>();

        public LogHandler Logger { get; set; }

        private AudienceMember<T> Requester { get; set; }

        private IEnumerable<AudienceMember<T>> Audience => this.audience.Values;

        public Tribune(IPerformer<T> performer) => this.performer = performer;

        public void JoinAudience(AudienceMember<T> member)
        {
            if (member == null)
                return;

            if (this.audience.ContainsKey(member.Key))
            {
                LeaveAudience(member.Key);
            }

            this.audience[member.Key] = member;
            this.OnAudienceJoined(member);
        }

        public void JoinAudience(object key, int priority, T request)
        {
            var audienceMember = new AudienceMember<T>(key, priority, request);
            this.JoinAudience(audienceMember);
        }

        public void LeaveAudience(AudienceMember<T> member)
        {
            if (this.audience.Remove(member.Key))
                this.OnAudienceLeft(member);
        }

        public void LeaveAudience(object key)
        {
            if (this.audience.TryGetValue(key, out var member))
                this.LeaveAudience(member);
        }

        public void Clear()
        {
            this.audience.Clear();
            this.Requester = null;
            this.performer.StopPerformance();
        }

        private void OnAudienceJoined(AudienceMember<T> newAudience)
        {
            if (this.Requester == null || this.Requester.Priority < newAudience.Priority)
                this.MakeRequest(newAudience);
        }

        private void OnAudienceLeft(AudienceMember<T> member)
        {
            if (member != this.Requester)
                return;

            if (this.Audience.IsEmpty())
            {
                this.Requester = null;
                this.performer.StopPerformance();
            }
            else
            {
                var highestPriority = this.Audience.ArgMax(audienceMember => audienceMember.Priority);
                this.MakeRequest(highestPriority);
            }
        }

        private void MakeRequest(AudienceMember<T> member)
        {
            this.Requester = member;
            this.performer.PerformRequest(this.Requester.Request);

            this.Logger?.Log($"Request {this.Requester.Request} by {this.Requester} performed by {this.performer}");
        }
    }
}