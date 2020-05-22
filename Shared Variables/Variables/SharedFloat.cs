using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
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
            [SerializeField, ShowIf(nameof(_useConstant))]
            private float _constantValue;

            /// <summary>
            /// The <see cref="SharedVariable{T}"/>.
            /// </summary>
            [SerializeField, HideIf(nameof(_useConstant))]
            private SharedFloat _variable = null;

            /// <summary>
            /// Whether to use constant instead of variable.
            /// </summary>
            [SerializeField] private bool _useConstant = false;

            /// <summary>
            /// Construct a new <see cref="SharedFloat.Reference"/>.
            /// </summary>
            public Reference() { }

            /// <summary>
            /// Construct a new <see cref="SharedFloat.Reference"/>.
            /// </summary>
            public Reference(float constantValue)
            {
                _constantValue = constantValue;
            }

            /// <summary>
            /// Construct a new <see cref="SharedFloat.Reference"/>.
            /// </summary>
            public Reference(SharedFloat variable)
            {
                _variable = variable;
            }


            ///<inheritdoc />
            protected override float ConstantValue => _constantValue;

            ///<inheritdoc />
            protected override SharedVariable<float> Variable => _variable;

            ///<inheritdoc />
            protected override bool UseConstant => _useConstant;
        }
    }
}