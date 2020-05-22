using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// <see cref="ScriptedEventBase{T}"/> that passes along a <see cref="Transform"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Events/Transform")]
    public class TransformEvent : ScriptedEventBase<Transform>
    {
    }
}