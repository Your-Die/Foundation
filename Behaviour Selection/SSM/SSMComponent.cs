using Mutiny.Foundation.Behaviours;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mutiny.Foundation.SSM
{
    public class SSMComponent : LateStartupBehaviour
    {
        [Tooltip("If set to true, the state machine will reset to the first state in the sequence when enabled.")]
        [SerializeField]
        private bool resetOnEnable;

        [Tooltip(
            "If set to true, this state machine will disable when it it backwards out of the first state of forwards out of the last state.")]
        [SerializeField]
        private bool disableOnBorderStates;

        [SerializeField] private bool findStatesInChildren;

        [SerializeField, ShowIf(nameof(findStatesInChildren))]
        private ISequentialState[] states;

        private int currentIndex;

        public void TransitionTo(int index)
        {
            this.states[this.currentIndex].TryExit();
            this.currentIndex = index;
            this.states[this.currentIndex].TryEnter();
        }

        public bool TryTransitionTo(int index)
        {
            if (index >= 0 && index < this.states.Length)
            {
                this.TransitionTo(index);
                return true;
            }

            if (this.disableOnBorderStates)
                this.enabled = false;

            return false;
        }

        private void Update()
        {
            var currentState = this.states[this.currentIndex];

            if (currentState.ShouldTransitionForward())
            {
                this.TryTransitionTo(this.currentIndex + 1);
            }
            else if (currentState.ShouldTransitionBackward())
            {
                this.TryTransitionTo(this.currentIndex - 1);
            }
            else
            {
                currentState.Tick();
            }
        }

        protected override void Start()
        {
            // In start instead of awake because it involves other game-objects.
            if (this.findStatesInChildren)
                this.states = this.GetComponentsInChildren<ISequentialState>();

            // We do the base.Start second because that's when lateEnable is called,
            // and we want to have our states initialized.
            base.Start();
        }

        protected override void LateEnable()
        {
            if (this.resetOnEnable)
                this.currentIndex = 0;

            this.states[this.currentIndex].TryEnter();
        }

        private void OnDisable() => this.states[this.currentIndex].TryExit();
    }
}