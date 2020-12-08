using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{    
    public class ReferenceInitializer : ChinchilladaBehaviour
    {
        [SerializeField] private IReferenceAsset referenceAsset;

        [SerializeField] private SearchStrategy referenceSearchStrategy = SearchStrategy.FindComponent;

        [ShowInInspector, ReadOnly] private object referenceValue;

        protected override void FindComponents()
        {
            base.FindComponents();

            var referenceType = this.referenceAsset.ReferenceType;
            this.referenceValue = this.referenceSearchStrategy.FindComponent(this.gameObject, referenceType);
            
            this.referenceAsset.SetValue(this.referenceValue);
        }
    }
}