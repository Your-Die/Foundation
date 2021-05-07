using System.Collections.Generic;
using System.Linq;
using Chinchillada;
using UnityEngine;

namespace Utilities.Common
{
    public class RaycastFinder<T> : ChinchilladaBehaviour, IProvider<T>
        where T : Component
    {
        [SerializeField, FindComponent] private Transform origin;

        [SerializeField] private float maxDistance = 100f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private QueryTriggerInteraction triggerInteraction;
        [SerializeField] private int hitCacheSize = 10;

        private RaycastHit[] hitCache;

        public IEnumerable<T> Provide()
        {
            var size = Physics.RaycastNonAlloc(
                this.origin.position,
                this.origin.forward,
                this.hitCache, this.maxDistance,
                this.layerMask,
                this.triggerInteraction
            );

            return this.hitCache
                .Take(size)
                .Select(c => c.collider.GetComponent<T>())
                .Where(Equality.NotNull);
        }

        protected override void Awake()
        {
            base.Awake();

            this.hitCache = new RaycastHit[this.hitCacheSize];
        }
    }
}