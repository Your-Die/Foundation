using System.Collections.Generic;
using System.Linq;
using Mutiny.Foundation.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Chinchillada.Foundation.States
{
    public class GenericSequentialState : ChinchilladaBehaviour, ISequentialState
    {
        [SerializeField] private UnityEvent enteredEvent;
        [SerializeField] private UnityEvent exitedEvent;

        [SerializeField] private List<ICondition> forwardTransitionConditions;
        [SerializeField] private List<ICondition> backwardTransitionConditions;
        
        public bool IsActive { get; private set; }
        public void Enter()
        {
            this.IsActive = true;
            this.enteredEvent.Invoke();
        }

        public void Exit()
        {
            this.IsActive = false;
            this.exitedEvent.Invoke();
        }

        public void Tick()
        {
        }

        public bool ShouldTransitionForward()
        {
            return this.ValidateConditions(this.forwardTransitionConditions);
        }

        public bool ShouldTransitionBackward()
        {
            return this.ValidateConditions(this.backwardTransitionConditions);
        }

        private bool ValidateConditions(IEnumerable<ICondition> conditions)
        {
            return conditions.All(condition => condition.Validate());
        }
    }
}