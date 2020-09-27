namespace Chinchillada.Foundation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    public static class GetComponentExtensions
    {
        public static bool TryGetComponent<T>(this Component context, out T component)
        {
            component = context.GetComponent<T>();
            return component != null;
        }

        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Component component)
        {
            Transform transform = component.transform;
            return transform.GetComponentsInDirectChildren<T>();
        }
        
        public static IEnumerable<Component> GetComponentsInDirectChildren(this Component component, Type type)
        {
            Transform transform = component.transform;
            return transform.GetComponentsInDirectChildren(type);
        }

        /// <summary>
        /// Tries to find a <see cref="Component"/> of the given type in the first layer of children of the <paramref name="gameObject"/>.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this GameObject gameObject)
        {
            Transform transform = gameObject.transform;
            return transform.GetComponentsInDirectChildren<T>();
        }

        /// <summary>
        /// Tries to find a <see cref="Component"/> of the given type in the first layer of children of the <paramref name="transform"/>.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Transform transform)
        {
            int childCount = transform.childCount;
            for (int index = 0; index < childCount; index++)
            {
                Transform childTransform = transform.GetChild(index);
                T component = childTransform.GetComponent<T>();

                if (component != null)
                    yield return component;
            }
        }

        public static IEnumerable<Component> GetComponentsInDirectChildren(this Transform transform, Type type)
        {
            int childCount = transform.childCount;
            for (int index = 0; index < childCount; index++)
            {
                var childTransform = transform.GetChild(index);
                var component = childTransform.GetComponent(type);

                if (component != null)
                    yield return component;
            }
        }

        public static IEnumerable<T> GetComponentsInChildLayer<T>(this Transform transform, int layerDepth)
        {
            var layer = transform.GetChildLayer(layerDepth);
            return layer.SelectMany(t => t.GetComponents<T>());
        }

        public static IEnumerable<Transform> GetChildLayer(this Transform transform, int layerDepth)
        {
            var layer = Enumerables.Single(transform);

            for (int i = 0; i < layerDepth; i++) 
                layer = layer.SelectMany(GetChildren);
            
            return layer;

            IEnumerable<Transform> GetChildren(Transform t)
            {
                foreach (Transform child in t)
                    yield return child;
            }
        }
    }
}