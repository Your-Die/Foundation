using UnityEngine;

namespace Chinchillada.Utilities
{
    [CreateAssetMenu(menuName = "Termination/Disable Component")]
    public class DisableComponentTerminator : ScriptableTerminator
    {
        public override void Terminate(IComponent component) => component.enabled = false;
    }
}