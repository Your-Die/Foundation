using Chinchillada.Foundation;
using UnityEngine;

public class SingletonInitializer : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private Transform parent;

    [SerializeField] private SingletonReference reference;

    [SerializeField] private bool destroyInitializer = true;

    private void Awake()
    {
        if (!this.reference.HasInstance)
            this.reference.Instance = Instantiate(this.prefab, this.parent);

        if (this.destroyInitializer)
            Destroy(this.gameObject);
    }
}