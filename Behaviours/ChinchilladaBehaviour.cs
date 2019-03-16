using Chinchillada.Utilities;
using Sirenix.OdinInspector;

namespace Mutiny.RitualPractice
{
    public class ChinchilladaBehaviour : SerializedMonoBehaviour
    {
        protected virtual void Awake()
        {
            FindComponents();
            HandleDefaultAssets();
        }

        protected virtual void OnValidate()
        {
            HandleDefaultAssets();
        }

        [Button]
        protected virtual void FindComponents()
        {
            AttributeHelper.ApplyAttribute<FindComponentAttribute>(this);
        }

        private void HandleDefaultAssets()
        {
            AttributeHelper.ApplyAttribute<DefaultAssetAttribute>(this);
        }
    }
}
