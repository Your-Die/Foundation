using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;
using UnityEngine;

namespace Utilities.Pooling
{
    public class FiniteObjectPool : GameObjectPoolBase
    {
        [SerializeField] private List<GameObject> prefabs = new List<GameObject>();

        [SerializeField] private Transform poolParent;

        public override GameObject Instantiate(Vector3 position, Transform parent = null)
        {
            if (this.prefabs.IsEmpty())
                return null;

            var prefab = this.prefabs.GrabRandom();
            return Instantiate(prefab, position, Quaternion.identity, parent);
        }

        public override T Instantiate<T>(Vector3 position, Transform parent = null)
        {
            var obj = this.Instantiate(position, parent);
            return obj == null ? default : obj.GetComponent<T>();
        }

        public override void Return(GameObject obj) => Destroy(obj);
    }
}