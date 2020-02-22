using Chinchillada;
using Chinchillada.Utilities;
using UnityEngine;

public abstract class GameObjectPoolBase : ChinchilladaBehaviour, IGameObjectPool
{
    public abstract GameObject Instantiate(Vector3 position, Transform parent = null);
    public abstract T Instantiate<T>(Vector3 position, Transform parent = null);
    public abstract void Return(GameObject obj);
}