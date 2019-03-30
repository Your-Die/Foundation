using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// <see cref="ScriptedEvent{T}"/> that passes along a <see cref="Transform"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Events/Transform")]
    public class TransformEvent : ScriptedEvent<Transform>
    {
    }
}