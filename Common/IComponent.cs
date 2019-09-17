using UnityEngine;

namespace Chinchillada
{
    public interface IComponent
    {
        bool enabled { get; set; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }

    public static class GetComponentExtensions
    {
        public static T GetComponent<T>(this IComponent component) => component.gameObject.GetComponent<T>();
        public static T[] GetComponents<T>(this IComponent component) => component.gameObject.GetComponents<T>();
        
        public static T GetComponentInChildren<T>(this IComponent component) 
            => component.gameObject.GetComponentInChildren<T>();
        public static T[] GetComponentsInChildren<T>(this IComponent component) 
            => component.gameObject.GetComponentsInChildren<T>();
    }
}