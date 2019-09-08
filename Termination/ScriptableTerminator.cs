using UnityEngine;

namespace Chinchillada.Utilities
{
    public abstract class ScriptableTerminator : ScriptableObject, ITerminator
    {
        public abstract void Terminate(IComponent component);
    }
}