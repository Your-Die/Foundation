using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Colors
{
    [RequireComponent(typeof(Renderer))]
    public class ColorSchemeApplier : ChinchilladaBehaviour, IColorschemeUser
    {
        [SerializeField] private int schemeIndex;

        [SerializeField, FindComponent] private Renderer rendererComponent;

        [SerializeField, FindComponent(SearchStrategy.InParent)]
        private IColorScheme scheme;
        
        public IColorScheme ColorScheme
        {
            get => scheme;
            set
            {
                scheme = value;
                ApplyScheme();
            }
        }

        public void ApplyScheme()
        {
            this.schemeIndex %= this.scheme.Count;
            var color = this.scheme[this.schemeIndex];

            this.rendererComponent.material.color = color;
        }

        protected override void Awake()
        {
            base.Awake();
            this.ApplyScheme();
        }

    }
}