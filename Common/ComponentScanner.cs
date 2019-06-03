using System.Collections.Generic;
using System.Linq;
using Chinchillada.Interactables;
using UnityEngine;

namespace Chinchillada.Utilities
{
    public class ComponentScanner<T> : ChinchilladaBehaviour, IProvider<T>
    where T : Component
    {
        [SerializeField, FindComponent] private Transform _center;

        [SerializeField] private float _radius = 10f;

        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private QueryTriggerInteraction _interaction;

        [SerializeField] private int _colliderCacheSize = 10;

        private Collider[] _colliderCache;
        
        public IEnumerable<T> Provide()
        {
            var size = Physics.OverlapSphereNonAlloc(_center.position, _radius, _colliderCache, _layerMask, _interaction);
            return _colliderCache
                .Take(size)
                .Select(c => c.GetComponent<T>())
                .Where(Equality.NotNull);
        }

        protected override void Awake()
        {
            base.Awake();
            
            _colliderCache = new Collider[_colliderCacheSize];
        }
    }
}