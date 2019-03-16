using System;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [RequireComponent(typeof(Collider))]
    public class CollisionEvents : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;
        public event Action<Collision> CollisionExited;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEntered?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExited?.Invoke(collision);
        }
    }
}