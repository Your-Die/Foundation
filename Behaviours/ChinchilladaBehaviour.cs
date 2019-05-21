using Chinchillada.Utilities;
using Sirenix.OdinInspector;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Base class for Monobehaviours. Inherits from <see cref="Sirenix.OdinInspector.SerializedMonoBehaviour"/>.
    /// Automatically applies <see cref="FindComponentAttribute"/> on awake, and also extends a Button to manually trigger it from the Unity editor.
    /// </summary>
    public class ChinchilladaBehaviour : SerializedMonoBehaviour
    {
        protected virtual void Awake()
        {
            FindComponents();
        }
        
        /// <summary>
        /// Applies the <see cref="FindComponentAttribute"/> on this <see cref="UnityEngine.MonoBehaviour"/>
        /// </summary>
        [Button]
        protected virtual void FindComponents()
        {
            AttributeHelper.ApplyAttribute<FindComponentAttribute>(this);
        }
    }
}
