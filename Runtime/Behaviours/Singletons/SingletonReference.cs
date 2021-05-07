using UnityEngine;

namespace Chinchillada
{
    [CreateAssetMenu(menuName = "Chinchillada/Singleton reference")]
    public class SingletonReference : ScriptableObject
    {
        public GameObject Instance { get; set; }

        public bool HasInstance => this.Instance != null;
    }
}