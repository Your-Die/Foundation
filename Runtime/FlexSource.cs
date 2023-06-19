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
        [SerializeField]
        [ShowIf(nameof(type), FlexVariableType.Constant)]
        private T value;

        [SerializeReference]
        [HideLabel]
        [ShowIf(nameof(type), FlexVariableType.Source)]
        private ISource<T> source;

        [SerializeReference]
        [HideLabel]
        [ShowIf(nameof(type), FlexVariableType.Generator)]
        private IGenerator<T> generator;

        public T Get()
        {
            switch (this.type)
            {
                case FlexVariableType.Constant:
                    return this.value;
                case FlexVariableType.Source:
                    return this.source.GetValue();
                case FlexVariableType.Generator:
                    return this.generator.Generate();
                default:
                    throw new NotSupportedException();
            }
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