namespace Chinchillada.Foundation
{
    using System;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    [InlineProperty]
    public class FlexSource<T>
    {
        [SerializeField]
        [HideLabel]
        [HorizontalGroup]
        public FlexVariableType type;

        [HorizontalGroup]
        [HideLabel]
        [OdinSerialize]
        [ShowIf(nameof(type), FlexVariableType.Constant)]
        private T value;

        [OdinSerialize]
        [HideLabel]
        [HorizontalGroup]
        [ShowIf(nameof(type), FlexVariableType.Source)]
        private ISource<T> source;

        [OdinSerialize]
        [HideLabel]
        [HorizontalGroup]
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