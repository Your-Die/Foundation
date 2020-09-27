using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.Foundation.States
{
    public class SequentialStateMachine : ISequentialStateMachine
    {
        private readonly IList<ISequentialState> states;

        public int CurrentIndex { get; private set; }

        public bool IsActive { get; private set; }

        public event Action BorderReached;

        public SequentialStateMachine(IList<ISequentialState> states) => this.states = states;

        public SequentialStateMachine(IEnumerable<ISequentialState> states) => this.states = states.ToList();

        public SequentialStateMachine() => this.states = new List<ISequentialState>();

        public void TransitionTo(int index)
        {
            this.states[this.CurrentIndex].TryExit();
            this.CurrentIndex = index;
            this.states[this.CurrentIndex].TryEnter();
        }

        public bool TryTransitionTo(int index)
        {
            if (index < 0 || index >= this.states.Count)
            {
                this.BorderReached?.Invoke();
                return false;
            }

            this.TransitionTo(index);
            return true;
        }

        public void Tick()
        {
            var currentState = this.states[this.CurrentIndex];

            if (currentState.ShouldTransitionForward())
            {
                this.TryTransitionTo(this.CurrentIndex + 1);
            }
            else if (currentState.ShouldTransitionBackward())
            {
                this.TryTransitionTo(this.CurrentIndex - 1);
            }
            else
            {
                currentState.Tick();
            }
        }

        public void Enter()
        {
            this.IsActive = true;
            this.CurrentIndex = 0;
        }

        public void Exit()
        {
            this.IsActive = false;
        }
        
        #region IList interface

        public IEnumerator<ISequentialState> GetEnumerator() => this.states.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.states).GetEnumerator();

        public void Add(ISequentialState item) => this.states.Add(item);

        public void Clear() => this.states.Clear();

        public bool Contains(ISequentialState item) => this.states.Contains(item);

        public void CopyTo(ISequentialState[] array, int arrayIndex) => this.states.CopyTo(array, arrayIndex);

        public bool Remove(ISequentialState item) => this.states.Remove(item);

        public int Count => this.states.Count;

        public bool IsReadOnly => this.states.IsReadOnly;

        public int IndexOf(ISequentialState item) => this.states.IndexOf(item);

        public void Insert(int index, ISequentialState item) => this.states.Insert(index, item);

        public void RemoveAt(int index) => this.states.RemoveAt(index);

        public ISequentialState this[int index]
        {
            get => this.states[index];
            set => this.states[index] = value;
        }
        
        #endregion
    }
}