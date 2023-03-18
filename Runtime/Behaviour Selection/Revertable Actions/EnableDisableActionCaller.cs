using UnityEngine;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{
    public class EnableDisableActionCaller : MonoBehaviour
    {
        [SerializeReference, Required] private IRevertableAction action;

        private void OnEnable() => this.action.Trigger();

        private void OnDisable() => this.action.Revert();
    }
}