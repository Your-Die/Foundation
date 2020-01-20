using UnityEngine;

public interface IGameObjectPool
{
    GameObject Instantiate(Vector3 position, Transform parent = null);
    T Instantiate<T>(Vector3 position, Transform parent = null);
    void Return(GameObject obj);
}