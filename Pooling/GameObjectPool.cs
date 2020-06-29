using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using UnityEngine;

public class GameObjectPool : GameObjectPoolBase
{
    [SerializeField, FindComponent] private Transform poolParent;

    [SerializeField] private GameObject prefab;

    private HashSet<GameObject> activeObjects = new HashSet<GameObject>();
    
    private readonly LinkedList<GameObject> inactiveObjects = new LinkedList<GameObject>();

    public override IReadOnlyCollection<GameObject> ActiveObjects => this.activeObjects;

    public override GameObject Instantiate(Vector3? location = null, Transform parent = null)
    {
        var position = location ?? Vector3.zero;

        var instance = this.inactiveObjects.Any()
            ? this.GetInactiveObject(position, parent)
            : Instantiate(this.prefab, position, Quaternion.identity, parent);

        this.activeObjects.Add(instance);
        return instance;
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

        this.activeObjects.Remove(obj);
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