using System.Collections.Generic;
using System.Linq;
using Chinchillada.Interactables;
using Chinchillada.Utilities;
using UnityEngine;

namespace Utilities.Common
{
    public class RaycastFinder<T> : ChinchilladaBehaviour, IProvider<T>
        where T : Component
    {
        [SerializeField, FindComponent] private Transform _origin;

        [SerializeField] private float _maxDistance = 100f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction;
        [SerializeField] private int _hitCacheSize = 10;

        private RaycastHit[] _hitCache;

        public IEnumerable<T> Provide()
        {
            var size = Physics.RaycastNonAlloc(_origin.position, _origin.forward, _hitCache, _maxDistance, _layerMask,
                _triggerInteraction);

            return _hitCache
                .Take(size)
                .Select(c => c.collider.GetComponent<T>())
                .Where(Equality.NotNull);
        }

        protected override void Awake()
        {
            base.Awake();

            _hitCache = new RaycastHit[_hitCacheSize];
        }
    }
}