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
        protected virtual T Value { get; set; }

        public Type ReferenceType => typeof(T);

        public void SetValue(object value)
        {
            this.Value = (T) value;
        }
    }
}