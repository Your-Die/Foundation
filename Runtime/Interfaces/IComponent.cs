using UnityEngine;

namespace Chinchillada
{
    public interface IComponent
    {
        bool enabled { get; set; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}