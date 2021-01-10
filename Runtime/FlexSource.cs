namespace Chinchillada.Foundation
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class FlexSource<T>
    {
        [SerializeField]
        [HideLabel]
        public FlexVariableType type;

        [HideLabel]
        [OdinSerialize]
        [ShowIf(nameof(type), FlexVariableType.Constant)]
        private T value;

        [OdinSerialize]
        [HideLabel]
        [ShowIf(nameof(type), FlexVariableType.Source)]
        private ISource<T> source;

        [OdinSerialize]
        [HideLabel]
        [ShowIf(nameof(type), FlexVariableType.Generator)]
        private IGenerator<T> generator;

        public T Get()
        {
            return this.type switch
            {
                FlexVariableType.Constant  => this.value,
                FlexVariableType.Source    => this.source.GetValue(),
                FlexVariableType.Generator => this.generator.Generate(),
                _                          => throw new NotSupportedException()
            };
        }

        public static implicit operator T(FlexSource<T> variable) => variable.Get();
    }

    public enum FlexVariableType
    {
        Constant,
        Source,
        Generator
    }
}