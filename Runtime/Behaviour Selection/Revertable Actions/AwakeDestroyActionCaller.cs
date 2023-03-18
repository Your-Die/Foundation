using UnityEngine;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{
    public class AwakeDestroyActionCaller : MonoBehaviour
    {
        [SerializeReference, Required] private IRevertableAction action;

        private void Awake() => this.action.Trigger();
        private void OnDestroy() => this.action.Revert();
    }
}