using UnityEngine;

namespace Chinchillada.Foundation
{
    [CreateAssetMenu(menuName = "Termination/Disable Component")]
    public class DisableComponentTerminator : ScriptableTerminator
    {
        public override void Terminate(IComponent component) => component.enabled = false;
    }
}