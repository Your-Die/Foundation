using UnityEngine;

namespace Chinchillada.Foundation
{
    [CreateAssetMenu(menuName = "Termination/Disable GameObject")]
    public class DisableObjectTerminator : ScriptableTerminator
    {
        public override void Terminate(IComponent component) => component.gameObject.SetActive(false);
    }
}