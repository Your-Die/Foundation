using System;
using System.Collections.Generic;
using Chinchillada.Foundation;
using UnityEngine;

public abstract class GameObjectPoolBase : ChinchilladaBehaviour, IGameObjectPool
{
    public abstract IReadOnlyCollection<GameObject> ActiveObjects { get; }
    public abstract event Action<GameObject> InstantiatedEvent;
    public abstract event Action<GameObject> ReturnedEvent;
    public abstract GameObject Instantiate(Vector3? position = null, Transform parent = null);
    public abstract T Instantiate<T>(Vector3? position = null, Transform parent = null);
    public abstract void Return(GameObject obj);
}