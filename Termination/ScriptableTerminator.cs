using UnityEngine;

namespace Chinchillada.Foundation
{
    public abstract class ScriptableTerminator : ScriptableObject, ITerminator
    {
        public abstract void Terminate(IComponent component);
    }
}