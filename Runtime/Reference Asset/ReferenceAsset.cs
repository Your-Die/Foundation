using Sirenix.OdinInspector;
using Type = System.Type;

namespace Chinchillada.Foundation
{
    public interface IReferenceAsset
    {
        Type ReferenceType { get; }

        void SetValue(object value);
    }
    
    public abstract class ReferenceAsset<T> : SerializedScriptableObject, IReferenceAsset
    {
        [field: ShowInInspector, ReadOnly]
        protected T Reference { get; set; }

        public Type ReferenceType => typeof(T);

        public void SetValue(object value)
        {
            this.Reference = (T) value;
        }
    }
}