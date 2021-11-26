using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada
{
    public class ComponentScanner<T> : AutoRefBehaviour, IProvider<T>
    where T : Component
    {
        [SerializeField, FindComponent] private Transform center;

        [SerializeField] private float radius = 10f;

        [SerializeField] private LayerMask layerMask;

        [SerializeField] private QueryTriggerInteraction interaction;

        [SerializeField] private int colliderCacheSize = 10;

        private Collider[] colliderCache;
        
        public IEnumerable<T> Provide()
        {
            var size = Physics.OverlapSphereNonAlloc(
                this.center.position, 
                this.radius, 
                this.colliderCache, 
                this.layerMask, 
                this.interaction);
            
            return this.colliderCache
                .Take(size)
                .Select(c => c.GetComponent<T>())
                .Where(Equality.NotNull);
        }

        protected override void Awake()
        {
            base.Awake();
            
            this.colliderCache = new Collider[this.colliderCacheSize];
        }
    }
}