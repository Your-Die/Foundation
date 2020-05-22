using System.Collections.Generic;
using System.Linq;
using Chinchillada;
using Chinchillada.Foundation;
using UnityEngine;

public class GameObjectPool : GameObjectPoolBase
{
    [SerializeField, FindComponent] private Transform poolParent;

    [SerializeField] private GameObject prefab;

    private readonly LinkedList<GameObject> inactiveObjects = new LinkedList<GameObject>();

    public override GameObject Instantiate(Vector3? location = null, Transform parent = null)
    {
        var position = location ?? Vector3.zero;
        
        return this.inactiveObjects.Any()
            ? this.GetInactiveObject(position, parent)
            : Instantiate(this.prefab, position, Quaternion.identity, parent);
    }

    public override T Instantiate<T>(Vector3? position = null, Transform parent = null)
    {
        var obj = this.Instantiate(position, parent);
        return obj.GetComponent<T>();
    }

    public override void Return(GameObject obj)
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