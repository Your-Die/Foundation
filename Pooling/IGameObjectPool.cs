using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameObjectPool
{
    IReadOnlyCollection<GameObject> ActiveObjects { get; }

    GameObject Instantiate(Vector3? position = null, Transform parent = null);
    T Instantiate<T>(Vector3? position = null, Transform parent = null);
    void Return(GameObject obj);
}

public static class PoolExtensions
{
    public static void ReturnAll(this IGameObjectPool pool)
    {
        foreach (var item in pool.ActiveObjects.ToList()) 
            pool.Return(item);
    }
}