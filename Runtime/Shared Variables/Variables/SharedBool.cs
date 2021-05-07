using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chinchillada
{
    /// <summary>
    /// Implementation of <see cref="SharedVariable{T}"/> for <see cref="bool"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Variables/Bool")]
    public class SharedBool : SharedVariable<bool>
    {
        /// <summary>
        /// Implementation of <see cref="VariableReference{T}"/> for <see cref="bool"/>.
        /// </summary>
        [Serializable]
        public class Reference : VariableReference<bool>
        {
            /// <summary>
            /// The constant value.
            /// </summary>
            [FormerlySerializedAs("_constantValue")] [SerializeField, ShowIf(nameof(useConstant))]
            private bool constantValue = false;

            /// <summary>
            /// The <see cref="SharedVariable{T}"/>.
            /// </summary>
            [FormerlySerializedAs("_variable")] [SerializeField, HideIf(nameof(useConstant))]
            private SharedBool variable = null;

            /// <summary>
            /// Whether to use constant instead of variable.
            /// </summary>
            [FormerlySerializedAs("_useConstant")] [SerializeField]
            private bool useConstant = false;

            /// <summary>
            /// Construct a new <see cref="SharedBool.Reference"/>.
            /// </summary>
            public Reference()
            {
            }

            /// <summary>
            /// Construct a new <see cref="SharedBool.Reference"/>.
            /// </summary>
            public Reference(bool constantValue) => this.constantValue = constantValue;

            public Reference(SharedBool variable) => this.variable = variable;

            ///<inheritdoc />
            protected override bool ConstantValue => this.constantValue;

            ///<inheritdoc />
            protected override SharedVariable<bool> Variable => this.variable;

            ///<inheritdoc />
            protected override bool UseConstant => this.useConstant;
        }
    }
}