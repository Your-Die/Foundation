using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Utilities
{
    [CreateAssetMenu(menuName = "Chinchillada/Variables/Bool")]
    public class SharedBool : SharedVariable<bool>
    {
        [Serializable]
        public class Reference : VariableReference<bool>
        {
            [SerializeField, ShowIf(nameof(_useConstant))]
            private bool _constantValue = false;

            [SerializeField, HideIf(nameof(_useConstant))]
            private SharedBool _variable = null;

            [SerializeField] private bool _useConstant = false;

            public Reference() { }

            public Reference(bool constantValue)
            {
                _constantValue = constantValue;
            }

            protected override bool ConstantValue => _constantValue;
            protected override SharedVariable<bool> Variable => _variable;
            protected override bool UseConstant => _useConstant;
        }
    }
}
