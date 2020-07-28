using UnityEngine;

namespace Chinchillada.Foundation
{
    public class BidirectionTweenComponent : ChinchilladaBehaviour, IBidirectionalTweener
    {
        [SerializeField] private IBidirectionalTweener tweener;
        public void TweenForward() => this.tweener.TweenForward();

        public void TweenBackward() => this.tweener.TweenBackward();

        public void ForceForward()
        {
            this.tweener.ForceForward();
        }

        public void ForceBackward()
        {
            this.tweener.ForceBackward();
        }
    }
}