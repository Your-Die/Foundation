using UnityEngine;

namespace Chinchillada.Utilities
{
    [CreateAssetMenu(menuName = "Termination/Destroy")]
    public class DestroyTerminator : ScriptableTerminator
    {
        public override void Terminate(IComponent component) => Destroy(component.gameObject);
    }
}