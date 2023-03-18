using System;
using DG.Tweening;
using UnityEngine;

namespace Chinchillada
{
    public class BidirectionTweenComponent : AutoRefBehaviour, IBidirectionalTweener
    {
        [SerializeReference] private IBidirectionalTweener tweener;
        
        public void TweenForward() => this.tweener.TweenForward();

        public void TweenBackward() => this.tweener.TweenBackward();
        public void TweenBackward(TweenCallback onFinished) => this.tweener.TweenBackward(onFinished);

        public void ForceForward() => this.tweener.ForceForward();

        public void ForceBackward() => this.tweener.ForceBackward();
        
        [Serializable]
        public class Reference : IBidirectionalTweener
        {
            [SerializeField] private BidirectionTweenComponent component;
            public void TweenForward() => this.component.TweenForward();

            public void TweenBackward() => this.component.TweenBackward();

            public void TweenBackward(TweenCallback onFinished) => this.component.TweenBackward(onFinished);

            public void ForceForward() => this.component.ForceForward();

            public void ForceBackward() => this.component.ForceBackward();
        }
    }
}