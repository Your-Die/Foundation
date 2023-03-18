using UnityEngine;
using Sirenix.OdinInspector;

namespace Chinchillada.Behavior
{
    public class AwakeAction : MonoBehaviour
    {
        [SerializeReference, Required] private IAction action;

        private void Awake() => this.action.Trigger();
    }
}