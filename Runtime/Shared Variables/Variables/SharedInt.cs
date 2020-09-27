using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
            [FormerlySerializedAs("_constantValue")] [SerializeField, ShowIf(nameof(useConstant))]
            private int constantValue;
            
            /// <summary>
            /// The <see cref="SharedVariable{T}"/>.
            /// </summary>
            [FormerlySerializedAs("_variable")] [SerializeField, HideIf(nameof(useConstant))]
            private SharedInt variable = null;

            /// <summary>
            /// Whether to use constant instead of variable.
            /// </summary>
            [FormerlySerializedAs("_useConstant")] [SerializeField] private bool useConstant = false;

            /// <summary>
            /// Construct a new <see cref="SharedInt.Reference"/>.
            /// </summary>
            /// 
            public Reference() { }

            /// <summary>
            /// Construct a new <see cref="SharedInt.Reference"/>.
            /// </summary>
            public Reference(int constantValue) => this.constantValue = constantValue;

            /// <summary>
            /// Construct a new <see cref="SharedInt.Reference"/>.
            /// </summary>
            public Reference(SharedInt variable) => this.variable = variable;

            ///<inheritdoc />
            protected override int ConstantValue => this.constantValue;

            ///<inheritdoc />
            protected override SharedVariable<int> Variable => this.variable;

            ///<inheritdoc />
            protected override bool UseConstant => this.useConstant;
        }
    }
}
