using System;
using System.Collections.Generic;
using Chinchillada.Interactables;
using Chinchillada.Utilities;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ComponentRange<T> : ChinchilladaBehaviour, IProvider<T>
    where T : Component
{
    private readonly LinkedList<T> _components = new LinkedList<T>();

    public IEnumerable<T> Provide()
    {
        _components.RemoveWhere(Equality.Null);
        return _components;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<T>(out var component)) 
            _components.AddLast(component);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<T>(out var component)) 
            _components.Remove(component);
    }
}
