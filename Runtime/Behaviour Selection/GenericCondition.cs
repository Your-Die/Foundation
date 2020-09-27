using UnityEngine;

namespace Chinchillada.Foundation
{
    public class GenericCondition : ChinchilladaBehaviour, ICondition
    {
        [SerializeField] private bool isSatisfied;

        public bool IsSatisfied
        {
            get => this.isSatisfied;
            set => this.isSatisfied = value;
        }

        public void Satisfy() => this.isSatisfied = true;

        public bool Validate() => this.isSatisfied;
    }
}