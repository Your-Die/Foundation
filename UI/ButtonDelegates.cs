using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chinchillada.Foundation.UI
{
    public class ButtonDelegates : ChinchilladaBehaviour
    {
        [SerializeField] private List<ButtonDelegate> buttonDelegates = new List<ButtonDelegate>();

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
            foreach (var buttonDelegate in this.buttonDelegates)
                buttonDelegate.Subscribe();
        }

        private void OnDisable()
        {
            foreach (var buttonDelegate in this.buttonDelegates)
                buttonDelegate.Unsubscribe();
        }

        [Serializable]
        private class ButtonDelegate
        {
            [SerializeField] private Button button;

            [SerializeField] private UnityEvent onClick;

            public Button Button
            {
                get => this.button;
                set => this.button = value;
            }

            public void Subscribe() => this.Button.onClick.AddListener(this.OnButtonClicked);

            public void Unsubscribe() => this.Button.onClick.RemoveListener(this.OnButtonClicked);

            private void OnButtonClicked() => this.onClick.Invoke();
        }
    }
}