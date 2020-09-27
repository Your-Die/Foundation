using System;
using DG.Tweening;
using Mutiny.Foundation.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    [Serializable]
    public class BidirectionalMoveTweener : IBidirectionalTweener
    {
        [SerializeField] private TransformMoveTween forwardTween;
        [SerializeField] private TransformMoveTween backwardTween;

        private Tweener tweener;

        [Button, HideInEditorMode]
        public void TweenForward() => this.Tween(this.forwardTween);

        [Button, HideInEditorMode]
        public void TweenBackward() => this.Tween(this.backwardTween);

        public void TweenBackward(TweenCallback onFinished)
        {
            this.TweenBackward();
            this.tweener.onComplete = onFinished;
        }

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