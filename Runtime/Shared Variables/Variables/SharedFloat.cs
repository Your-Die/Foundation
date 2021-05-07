using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chinchillada
{
    /// <summary>
    /// Implementation of <see cref="SharedVariable{T}"/> for <see cref="float"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Variables/Float")]
    public class SharedFloat : SharedVariable<float>
    {
        /// <summary>
        /// Implementation of <see cref="VariableReference{T}"/> for <see cref="float"/>.
        /// </summary>
        [Serializable]
        public class Reference : VariableReference<float>
        {
            /// <summary>
            /// The constant value.
            /// </summary>
            [FormerlySerializedAs("_constantValue")] [SerializeField, ShowIf(nameof(useConstant))]
            private float constantValue;

            /// <summary>
            /// The <see cref="SharedVariable{T}"/>.
            /// </summary>
            [FormerlySerializedAs("_variable")] [SerializeField, HideIf(nameof(useConstant))]
            private SharedFloat variable = null;

            /// <summary>
            /// Whether to use constant instead of variable.
            /// </summary>
            [FormerlySerializedAs("_useConstant")] [SerializeField] private bool useConstant = false;

            /// <summary>
            /// Construct a new <see cref="SharedFloat.Reference"/>.
            /// </summary>
            public Reference() { }

            /// <summary>
            /// Construct a new <see cref="SharedFloat.Reference"/>.
            /// </summary>
            public Reference(float constantValue) => this.constantValue = constantValue;

            /// <summary>
            /// Construct a new <see cref="SharedFloat.Reference"/>.
            /// </summary>
            public Reference(SharedFloat variable) => this.variable = variable;


            ///<inheritdoc />
            protected override float ConstantValue => this.constantValue;

            ///<inheritdoc />
            protected override SharedVariable<float> Variable => this.variable;

            ///<inheritdoc />
            protected override bool UseConstant => this.useConstant;
        }
    }
}