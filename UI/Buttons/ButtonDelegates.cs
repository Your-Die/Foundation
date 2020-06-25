using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chinchillada.Foundation.UI
{
    /// <summary>
    /// Collects all child buttons so the <see cref="Button.onClick"/> events can be edited in one place.
    /// </summary>
    public class ButtonDelegates : ChinchilladaBehaviour
    {
        /// <summary>
        /// The list of button delegates.
        /// </summary>
        [SerializeField] private List<ButtonDelegate> buttonDelegates = new List<ButtonDelegate>();
        
        /// <summary>
        /// Finds all buttons on children below this <see cref="GameObject"/>.
        /// </summary>
        [Button]
        private void FindButtons()
        {
            var newButtons = this.GetComponentsInChildren<Button>().Where(IsNew);

            foreach (var button in newButtons.ToArray())
            {
                var buttonDelegate = new ButtonDelegate {Button = button};
                this.buttonDelegates.Add(buttonDelegate);
            }

            bool IsNew(Button button) => this.buttonDelegates.All(buttonDelegate => buttonDelegate.Button != button);
        }

        private void OnEnable()
        {
            // Subscribe to all the button events.
            foreach (var buttonDelegate in this.buttonDelegates)
                buttonDelegate.Subscribe();
        }

        private void OnDisable()
        {
            // Unsubscribe from all the button events.
            foreach (var buttonDelegate in this.buttonDelegates)
                buttonDelegate.Unsubscribe();
        }

        /// <summary>
        /// Delegated <see cref="UnityEvent"/> for a <see cref="Button"/> onClick event.
        /// </summary>
        [Serializable]
        private class ButtonDelegate
        {
            /// <summary>
            /// The button.
            /// </summary>
            [SerializeField] private Button button;

            /// <summary>
            /// The delegate event.
            /// </summary>
            [SerializeField] private UnityEvent onClick;

            /// <summary>
            /// The button.
            /// </summary>
            public Button Button
            {
                get => this.button;
                set => this.button = value;
            }

            /// <summary>
            /// Makes this delegate subscribe to the <see cref="Button"/> onClick event.
            /// </summary>
            public void Subscribe() => this.Button.onClick.AddListener(this.OnButtonClicked);

            /// <summary>
            /// Makes this delegate unsubscribe from the <see cref="Button"/> onClick event.
            /// </summary>
            public void Unsubscribe() => this.Button.onClick.RemoveListener(this.OnButtonClicked);

            /// <summary>
            /// Invoked when the <see cref="Button"/> onClick event is invoked.
            /// Invokes <see cref="onClick"/>.
            /// </summary>
            private void OnButtonClicked() => this.onClick.Invoke();
        }
    }
}