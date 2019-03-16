using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionUnityEvents2D : MonoBehaviour
    {
        [SerializeField] private UnityEvent _collisionEntered = new UnityEvent();
        [SerializeField] private UnityEvent _collisionExited = new UnityEvent();

        public UnityEvent CollisionEntered => _collisionEntered;
        public UnityEvent CollisionExited => _collisionExited;

        private void OnCollisionExit2D(Collision2D collision)
        {
            _collisionEntered.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _collisionExited.Invoke();
        }
    }
}