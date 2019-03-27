using Chinchillada.Utilities;
using Sirenix.OdinInspector;

namespace Mutiny.RitualPractice
{
    public class ChinchilladaBehaviour : SerializedMonoBehaviour
    {
        protected virtual void Awake()
        {
            FindComponents();
        }
        
        [Button]
        protected virtual void FindComponents()
        {
            AttributeHelper.ApplyAttribute<FindComponentAttribute>(this);
        }
    }
}
