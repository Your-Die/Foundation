using System;
using DG.Tweening;
using Mutiny.Foundation.Common;
using UnityEngine;

namespace Chinchillada.Foundation
{
    [Serializable]
    public class BidirectionalMoveTweener : IBidirectionalTweener
    {
        [SerializeField] private TransformMoveTween forwardTween;
        [SerializeField] private TransformMoveTween backwardTween;

        private Tweener tweener;

        public void TweenForward() => this.Tween(this.forwardTween);

        public void TweenBackward() => this.Tween(this.backwardTween);

        public void ForceForward() => this.Force(this.forwardTween);

        public void ForceBackward() => this.Force(this.backwardTween);

        private void Tween(TransformMoveTween tween)
        {
            this.tweener?.Kill();
            this.tweener = tween.Tween();
        }

        private void Force(TransformMoveTween tween)
        {
            this.tweener?.Kill();
            tween.CompleteImmediate();
        }
    }
}