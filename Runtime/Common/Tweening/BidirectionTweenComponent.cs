using DG.Tweening;
using UnityEngine;

namespace Chinchillada
{
    public class BidirectionTweenComponent : ChinchilladaBehaviour, IBidirectionalTweener
    {
        [SerializeField] private IBidirectionalTweener tweener;
        public void TweenForward() => this.tweener.TweenForward();

        public void TweenBackward() => this.tweener.TweenBackward();
        public void TweenBackward(TweenCallback onFinished) => this.tweener.TweenBackward(onFinished);

        public void ForceForward() => this.tweener.ForceForward();

        public void ForceBackward() => this.tweener.ForceBackward();
    }
}