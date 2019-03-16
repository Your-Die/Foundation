using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Utilities
{
    /// <summary>
    /// Float implementation of <see cref="SharedVariable{T}"/>
    /// </summary>
    [CreateAssetMenu(menuName = "Chinchillada/Variables/Float")]
    public class SharedFloat : SharedVariable<float>
    {
        [Serializable]
        public class Reference : VariableReference<float>
        {
            [SerializeField, ShowIf(nameof(_useConstant))]
            private float _constantValue;

            [SerializeField, HideIf(nameof(_useConstant))]
            private SharedFloat _variable = null;

            [SerializeField] private bool _useConstant = false;

            public Reference() { }

            public Reference(float constantValue)
            {
                _constantValue = constantValue;
            }

            protected override float ConstantValue => _constantValue;
            protected override SharedVariable<float> Variable => _variable;
            protected override bool UseConstant => _useConstant;
        }
    }
}