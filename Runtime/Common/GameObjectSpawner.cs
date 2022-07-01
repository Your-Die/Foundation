namespace Mouthsound.TimeShot
{
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Serializable]
    public class GameObjectSpawner : ISpawner<GameObject>
    {
        [SerializeField] private GameObject prefab;

        [SerializeField] private Transform parent;
        
        public GameObject Spawn(Vector3 position) => this.Spawn(position, Quaternion.identity);

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            return Object.Instantiate(this.prefab, position, rotation, this.parent);
        }
    }
}