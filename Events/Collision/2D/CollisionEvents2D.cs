using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionEvents2D : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEntered;
        public event Action<Collision2D> CollisionExited;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEntered?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExited?.Invoke(collision);
        }
    }
}