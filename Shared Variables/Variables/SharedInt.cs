using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    [CreateAssetMenu(menuName = "Chinchillada/Variables/Integer")]
    public class SharedInt : SharedVariable<int>
    {
        [Serializable]
        public class Reference : VariableReference<int>
        {
            /// <summary>
            /// The constant value.
            /// </summary>
            [SerializeField, ShowIf(nameof(_useConstant))]
            private int _constantValue;
            
            /// <summary>
            /// The <see cref="SharedVariable{T}"/>.
            /// </summary>
            [SerializeField, HideIf(nameof(_useConstant))]
            private SharedInt _variable = null;

            /// <summary>
            /// Whether to use constant instead of variable.
            /// </summary>
            [SerializeField] private bool _useConstant = false;

            /// <summary>
            /// Construct a new <see cref="SharedInt.Reference"/>.
            /// </summary>
            /// 
            public Reference() { }

            /// <summary>
            /// Construct a new <see cref="SharedInt.Reference"/>.
            /// </summary>
            public Reference(int constantValue)
            {
                _constantValue = constantValue;
            }

            /// <summary>
            /// Construct a new <see cref="SharedInt.Reference"/>.
            /// </summary>
            public Reference(SharedInt variable)
            {
                _variable = variable;
            }

            ///<inheritdoc />
            protected override int ConstantValue => _constantValue;

            ///<inheritdoc />
            protected override SharedVariable<int> Variable => _variable;

            ///<inheritdoc />
            protected override bool UseConstant => _useConstant;
        }
    }
}
