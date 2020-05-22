using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
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
            [SerializeField, ShowIf(nameof(_useConstant))]
            private bool _constantValue = false;

            /// <summary>
            /// The <see cref="SharedVariable{T}"/>.
            /// </summary>
            [SerializeField, HideIf(nameof(_useConstant))]
            private SharedBool _variable = null;

            /// <summary>
            /// Whether to use constant instead of variable.
            /// </summary>
            [SerializeField] private bool _useConstant = false;

            /// <summary>
            /// Construct a new <see cref="SharedBool.Reference"/>.
            /// </summary>
            public Reference() { }

            /// <summary>
            /// Construct a new <see cref="SharedBool.Reference"/>.
            /// </summary>
            public Reference(bool constantValue)
            {
                _constantValue = constantValue;
            }

            public Reference(SharedBool variable)
            {
                _variable = variable;
            }

            ///<inheritdoc />
            protected override bool ConstantValue => _constantValue;

            ///<inheritdoc />
            protected override SharedVariable<bool> Variable => _variable;

            ///<inheritdoc />
            protected override bool UseConstant => _useConstant;
        }
    }
}
