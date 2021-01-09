using Sirenix.OdinInspector;
using Type = System.Type;

namespace Chinchillada.Foundation
{
    public interface IReference
    {
        Type ReferenceType { get; }

        void SetValue(object value);
    }

    public interface IReference<T> : IReference
    {
        T Reference { get; set; }
    }
    
    public abstract class ReferenceAsset<T> : SerializedScriptableObject, IReference<T>, ISource<T>
    {
        [field: ShowInInspector, ReadOnly]
        public T Reference { get; set; }

        public Type ReferenceType => typeof(T);

        public T GetValue() => this.Reference;


        public void SetValue(object value) => this.Reference = (T) value;
    }
}