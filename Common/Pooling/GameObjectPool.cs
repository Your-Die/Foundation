using System.Collections.Generic;
using System.Linq;
using Chinchillada;
using Chinchillada.Utilities;
using UnityEngine;

public class GameObjectPool : ChinchilladaBehaviour, IGameObjectPool
{
    [SerializeField, FindComponent] private Transform poolParent;

    [SerializeField] private GameObject prefab;

    private readonly LinkedList<GameObject> inactiveObjects = new LinkedList<GameObject>();

    public GameObject Instantiate(Vector3 position, Transform parent = null)
    {
        return this.inactiveObjects.Any()
            ? this.GetInactiveObject(position, parent)
            : Instantiate(this.prefab, position, Quaternion.identity, parent);
    }

    public T Instantiate<T>(Vector3 position, Transform parent = null)
    {
        var obj = this.Instantiate(position, parent);
        return obj.GetComponent<T>();
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = this.poolParent;

        this.inactiveObjects.AddFirst(obj);
    }

    private GameObject GetInactiveObject(Vector3 position, Transform parent)
    {
        var inactiveObject = this.inactiveObjects.GrabFirst();
        inactiveObject.transform.position = position;
        inactiveObject.transform.parent = parent;
        inactiveObject.SetActive(true);

        return inactiveObject;
    }
}