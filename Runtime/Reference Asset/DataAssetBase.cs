using UnityEngine;

namespace Chinchillada.Foundation
{
    /// <summary>
    /// Implementation of <see cref="ReferenceAsset{T}"/> that delegates it's value to a serialized field.
    /// Inherit this for "data-assets", types that you can author at edit time.
    /// If you want to adjust the "instance" at runtime, consider using <see cref="SharedVariable{T}"/> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataAssetBase<T> : ReferenceAsset<T>
    {
        [SerializeField] private T value;

        public override T Reference
        {
            get => this.value;
            set => this.value = value;
        }
    }
}