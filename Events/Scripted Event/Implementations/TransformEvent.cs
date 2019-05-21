﻿using UnityEngine;

namespace Chinchillada.Events
{
    /// <summary>
    /// <see cref="ScriptedEvent{T}"/> that passes along a <see cref="Transform"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Events/Transform")]
    public class TransformEvent : ScriptedEvent<Transform>
    {
    }
}