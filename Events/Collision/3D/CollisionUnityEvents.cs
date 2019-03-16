using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    public class CollisionUnityEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent _collisionEntered;
        [SerializeField] private UnityEvent _collisionExited;

        public UnityEvent CollisionEntered => _collisionEntered;
        public UnityEvent CollisionExited => _collisionExited;

        private void OnCollisionEnter(Collision _)
        {
            _collisionEntered.Invoke();
        }

        private void OnCollisionExit(Collision _)
        {
            _collisionExited.Invoke();
        }
    }
}
