using Chinchillada;
using Chinchillada.Utilities;
using DG.Tweening;
using UnityEngine;

namespace Mutiny.Foundation.Common
{
    public class TransformMoveTween : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent] private Transform target;

        [SerializeField] private Transform endLocation;

        [SerializeField] private float duration;

        [SerializeField] private Ease ease;

        private Tweener tweener;
        
        public Tweener Tween()
        {
            this.Kill();
            this.tweener = this.target.DOMove(this.endLocation.position, this.duration).SetEase(this.ease);

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