using System;
using Chinchillada.Foundation;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mutiny.Foundation.Common
{
    public class TransformMoveTween : ChinchilladaBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField, FindComponent] private Transform endLocation;

        [SerializeField] private float duration = 0.3f;

        [SerializeField] private Ease ease = Ease.InOutSine;

        private Tweener tweener;
        
        [Button]
        public Tweener Tween()
        {
            this.Kill();
            this.tweener = this.target.DOMove(this.endLocation.position, this.duration).SetEase(this.ease);

            return this.tweener;
        }

        public Tweener Tween(TweenCallback onFinished)
        {
            this.Tween();
            this.tweener.onComplete = onFinished;

            return this.tweener;
        }

        public void CompleteImmediate()
        {
            this.Kill();
            
            this.target.position = this.endLocation.position;
        }

        public void Kill() => this.tweener?.Kill();
    }
}