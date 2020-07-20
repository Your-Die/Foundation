using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using Chinchillada.Foundation.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Mutiny.Thesis.UI
{
    /// <summary>
    /// Generic implementation of <see cref="IMultipleChoicePresenter{T}"/> that hides the type implementation behind
    /// a <see cref="IOption"/> interface.
    /// </summary>
    public class OptionPresenter : ChinchilladaBehaviour, IMultipleChoicePresenter<IOption>
    {
        /// <summary>
        /// The buttons used to present the <see cref="IOption"/>.
        /// </summary>
        [SerializeField] private UPoolingList<ButtonController> buttons = new UPoolingList<ButtonController>();

        private const string EventFoldoutGroup = "Events";
        [SerializeField, FoldoutGroup(EventFoldoutGroup)] private UnityEvent presentEvent;
        [SerializeField, FoldoutGroup(EventFoldoutGroup)] private UnityEvent hideEvent;
        
        private readonly Dictionary<ButtonController, IOption> optionLookup
            = new Dictionary<ButtonController, IOption>();

        private readonly Dictionary<ButtonController, ButtonListener> listenerLookup
            = new Dictionary<ButtonController, ButtonListener>();

        /// <summary>
        /// Callback invoked when an <see cref="IOption"/> has been selected.
        /// </summary>
        private Action<IOption> selectionCallback;

        /// <summary>
        /// Event invoked when an <see cref="IOption"/> has been selected.
        /// </summary>
        public event Action<IOption> SelectedEvent;

        /// <inheritdoc />
        public IEnumerable<IOption> Content { get; private set; }

        /// <inheritdoc />
        public void Present(IEnumerable<IOption> content)
        {
            var contentList = content.ToList();
            this.Content = contentList;
            this.buttons.ApplyWith(contentList, PresentOption);

            this.presentEvent.Invoke();
            
            void PresentOption(IOption option, ButtonController button)
            {
                option.Present(button);
                this.optionLookup[button] = option;
            }
        }

        /// <summary>
        /// Presents the <see cref="options"/> and invokes <see cref="onOptionSelected"/> when an option is selected.
        /// </summary>
        public void Present(IEnumerable<IOption> options, Action<IOption> onOptionSelected)
        {
            this.selectionCallback = onOptionSelected;
            this.Present(options);
        }

        /// <inheritdoc />
        public void Hide()
        {
            this.selectionCallback = null;

            this.optionLookup.Clear();
            this.buttons.Clear();
            
            this.hideEvent.Invoke();
        }

        protected override void Awake()
        {
            base.Awake();

            this.buttons.ItemActivated += this.OnButtonActivated;
            this.buttons.ItemDeactivated += this.OnButtonDeactivated;
        }

        private void OnDestroy()
        {
            this.buttons.ItemActivated -= this.OnButtonActivated;
            this.buttons.ItemDeactivated -= this.OnButtonDeactivated;
        }

        /// <summary>
        /// Invoked when a new button is added to the <see cref="buttons"/>.
        /// </summary>
        private void OnButtonActivated(ButtonController button)
        {
            // Create a new button listener.
            this.listenerLookup[button] = new ButtonListener(button, this.OnButtonClicked);
        }

        /// <summary>
        /// Called when a button is deactivated out of <see cref="buttons"/>.
        /// </summary>
        private void OnButtonDeactivated(ButtonController button)
        {
            // Deactivate and remove the listener.
            var listener = this.listenerLookup[button];
            listener.Deactivate();
            this.listenerLookup.Remove(button);
        }

        /// <summary>
        /// Called when one of the <see cref="buttons"/> is clicked.
        /// </summary>
        /// <param name="button">The clicked button.</param>
        private void OnButtonClicked(ButtonController button)
        {
            // Get the corresponding option.
            var option = this.optionLookup[button];

            // Invoke events.
            this.selectionCallback?.Invoke(option);
            this.SelectedEvent?.Invoke(option);
        }

        /// <summary>
        /// Listens to a button, and calls a callback with the button reference when the button is clicked.
        /// </summary>
        private class ButtonListener
        {
            private readonly Action<ButtonController> callback;
            private readonly ButtonController button;

            public ButtonListener(ButtonController button, Action<ButtonController> callback)
            {
                this.button = button;
                this.callback = callback;

                button.Button.onClick.AddListener(this.OnButtonClicked);
            }

            public void Deactivate() => this.button.Button.onClick.RemoveListener(this.OnButtonClicked);

            private void OnButtonClicked() => this.callback.Invoke(this.button);
        }
    }
}