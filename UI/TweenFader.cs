using System;
using System.Collections;
using Chinchillada.Foundation;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mutiny.UI
{
    public class TweenFader : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private Image fadeImage;

        [SerializeField] private FadeTween fadeIn;

        [SerializeField] private FadeTween fadeOut;

        private Tweener tweener;

        private IEnumerator routine;

        [Button]
        public void StartFadeIn() => this.StartFade(this.FadeIn());

        [Button]
        public void StartFadeOut() => this.StartFade(this.FadeOut());

        public IEnumerator FadeIn() => this.Fade(this.fadeIn);

        public IEnumerator FadeOut() => this.Fade(this.fadeOut);

        private IEnumerator Fade(FadeTween fadeTweener)
        {
            this.tweener = fadeTweener.DoTween(this.fadeImage);
            yield return this.tweener.WaitForCompletion();
        }

        private void StartFade(IEnumerator fadeRoutine)
        {
            this.tweener?.Kill();

            if (this.routine != null)
                this.StopCoroutine(this.routine);

            this.routine = fadeRoutine;
            this.StartCoroutine(this.routine);
        }

        [Serializable]
        private class FadeTween
        {
            [SerializeField] private float duration = 0.3f;

            [SerializeField] private Color targetColor = Color.black;

            [SerializeField] private Ease ease;

            [SerializeField] private UnityEvent startEvent;
            [SerializeField] private UnityEvent completionEvent;
            
            public Tweener DoTween(Image image)
            {
                this.startEvent.Invoke();
                
                var tweener =image.DOColor(this.targetColor, this.duration).SetEase(this.ease);
                tweener.onComplete = this.OnComplete;

                return tweener;
            }

            private void OnComplete() => this.completionEvent?.Invoke();
        }
    }
}