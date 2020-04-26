using UnityEngine;

public interface IGameObjectPool
{
    GameObject Instantiate(Vector3? position = null, Transform parent = null);
    T Instantiate<T>(Vector3? position = null, Transform parent = null);
    void Return(GameObject obj);
}