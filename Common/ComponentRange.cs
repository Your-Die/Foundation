using System.Collections.Generic;
using Chinchillada.Foundation;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ComponentRange<T> : ChinchilladaBehaviour, IProvider<T>
    where T : Component
{
    private readonly LinkedList<T> components = new LinkedList<T>();

    public IEnumerable<T> Provide()
    {
        this.components.RemoveWhere(Equality.Null).Enumerate();
        return this.components;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<T>(out var component)) 
            this.components.AddLast(component);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<T>(out var component)) 
            this.components.Remove(component);
    }
}
