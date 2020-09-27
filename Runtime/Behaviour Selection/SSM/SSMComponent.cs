using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation.States
{
    /// <summary>
    /// Humble object <see cref="Component"/> wrapper for <see cref="SequentialStateMachine"/>.
    /// </summary>
    public class SSMComponent : LateStartupBehaviour, ISequentialStateMachine
    {
        [Tooltip(
            "If set to true, this state machine will disable when" +
            " it it backwards out of the first state or forwards out of the last state.")]
        [SerializeField]
        private bool disableOnBorderStates;
        
        [Tooltip("Whether to find states in children.")]
        [SerializeField] private bool findStatesInChildren;

        [SerializeField, ShowIf(nameof(findStatesInChildren))]
        private ISequentialState[] initialStates;

        private SequentialStateMachine stateMachine;

        private void Update() => this.stateMachine.Tick();

        protected override void FindComponents()
        {
            base.FindComponents();
            if (this.findStatesInChildren)
                this.initialStates = this.GetComponentsInChildren<ISequentialState>();
            
        }

        protected override void Start()
        {
            this.stateMachine = new SequentialStateMachine(this.initialStates);
            this.stateMachine.BorderReached += this.OnBorderReached;
            
            // We do the base.Start second because that's when lateEnable is called,
            // and we want to have our states initialized.
            base.Start();
        }

        private void OnBorderReached()
        {
            if (this.disableOnBorderStates)
                this.enabled = false;
        }

        protected override void LateEnable() => this.stateMachine.Enter();

        private void OnDisable() => this.stateMachine.Exit();
        
        #region ISequentialStateMachine delegates
        
        public int CurrentIndex => this.stateMachine.CurrentIndex;

        public void TransitionTo(int index) => this.stateMachine.TransitionTo(index);

        public bool TryTransitionTo(int index) => this.stateMachine.TryTransitionTo(index);
        public event Action BorderReached
        {
            add => this.stateMachine.BorderReached += value;
            remove => this.stateMachine.BorderReached -= value;
        }
        
        public void Tick() => this.stateMachine.Tick();

        public bool IsActive => this.stateMachine.IsActive;

        public void Enter() => this.stateMachine.Enter();

        public void Exit() => this.stateMachine.Exit();
        
        public void Add(ISequentialState item) => this.stateMachine.Add(item);

        public void Clear() => this.stateMachine.Clear();

        public bool Contains(ISequentialState item) => this.stateMachine.Contains(item);

        public void CopyTo(ISequentialState[] array, int arrayIndex) => this.stateMachine.CopyTo(array, arrayIndex);

        public bool Remove(ISequentialState item) => this.stateMachine.Remove(item);

        public int Count => this.stateMachine.Count;

        public bool IsReadOnly => this.stateMachine.IsReadOnly;

        public int IndexOf(ISequentialState item) => this.stateMachine.IndexOf(item);

        public void Insert(int index, ISequentialState item) => this.stateMachine.Insert(index, item);

        public void RemoveAt(int index) => this.stateMachine.RemoveAt(index);

        public ISequentialState this[int index]
        {
            get => this.stateMachine[index];
            set => this.stateMachine[index] = value;
        }
        
        public IEnumerator<ISequentialState> GetEnumerator() => this.stateMachine.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.stateMachine).GetEnumerator();

        #endregion
    }
}