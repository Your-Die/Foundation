using Chinchillada;
using Chinchillada.Foundation;
using UnityEngine;

namespace Submodules.Foundation.Common
{
    public class Source<T> : ChinchilladaBehaviour, ISource<T>
    {
        [SerializeField] private T content;

        public T Content => this.content;

        public static implicit operator T(Source<T> source) => source.content;
    }
}