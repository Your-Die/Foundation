using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [CreateAssetMenu(menuName = "Chinchillada/Variables/Integer")]
    public class SharedInt : SharedVariable<int>
    {
        [Serializable]
        public class Reference : VariableReference<int>
        {
            [SerializeField, ShowIf(nameof(_useConstant))]
            private int _constantValue;

            [SerializeField, HideIf(nameof(_useConstant))]
            private SharedInt _variable = null;

            [SerializeField] private bool _useConstant = false;

            public Reference() { }

            public Reference(int constantValue)
            {
                _constantValue = constantValue;
            }

            protected override int ConstantValue => _constantValue;
            protected override SharedVariable<int> Variable => _variable;
            protected override bool UseConstant => _useConstant;
        }
    }
}
