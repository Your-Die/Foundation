namespace Chinchillada
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    public static class ComponentExtensions
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

        public static IEnumerable<T> GetComponentsInOnlyChildren<T>(this Transform target, 
            SearchStrategy strategy = SearchStrategy.FindComponent)
        {
            var queue = new Queue<Transform>();
            AddChildren(target);

            while (queue.Any())
            {
                var transform = queue.Dequeue();
                var components = strategy.FindComponents<T>(transform.gameObject);

                foreach (var component in components)
                    yield return component;
                
                AddChildren(transform);
            }
            
            void AddChildren(Transform transform)
            {
                for (var index = 0; index < transform.childCount; index++)
                {
                    var child = transform.GetChild(index);
                    queue.Enqueue(child);
                }
            }
        }

        public static IEnumerable<Component> GetComponentsInOnlyChildren(this Transform target, Type type,
            SearchStrategy strategy = SearchStrategy.FindComponent)
        {
            var queue = new Queue<Transform>();
            AddChildren(target);

            while (queue.Any())
            {
                var transform = queue.Dequeue();
                var components = strategy.FindComponents(transform.gameObject, type);

                foreach (var component in components)
                    yield return component;
                
                AddChildren(transform);
            }
            
            void AddChildren(Transform transform)
            {
                for (var index = 0; index < transform.childCount; index++)
                {
                    var child = transform.GetChild(index);
                    queue.Enqueue(child);
                }
            }
        }

        public static T GetComponentInOnlyChildren<T>(this Transform transform)
        {
            return transform.GetComponentsInOnlyChildren<T>().First();
        }
        public static Component GetComponentInOnlyChildren(this Transform transform, Type type)
        {
            return transform.GetComponentsInOnlyChildren(type).First();
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
        
        public static T GetComponentInScene<T>(this GameObject gameObject)
        {
            return gameObject.GetComponentsInScene<T>().First();
        }
        
        public static IEnumerable<T> GetComponentsInScene<T>(this GameObject gameObject)
        {
            var rootObjects = gameObject.scene.GetRootGameObjects();
            foreach (var root in rootObjects)
            {
                var components = root.GetComponentsInChildren<T>();
                foreach (var component in components)
                    yield return component;
            }
        }
    }
}