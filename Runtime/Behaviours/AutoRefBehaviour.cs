﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada
{
    /// <summary>
    /// Base class for MonoBehaviours. Inherits from <see cref="Sirenix.OdinInspector.SerializedMonoBehaviour"/>.
    /// Automatically applies <see cref="FindComponentAttribute"/> on awake, and also extends a Button to manually trigger it from the Unity editor.
    /// </summary>
    public abstract class AutoRefBehaviour : SerializedMonoBehaviour, IComponent
    {
        protected virtual void Awake()
        {
            this.FindComponents();
        }

        /// <summary>
        /// Applies the <see cref="FindComponentAttribute"/> on this <see cref="UnityEngine.MonoBehaviour"/>
        /// </summary>
        [Button]
        protected virtual void FindComponents()
        {
            ComponentFinderAttribute.ApplyAttribute<ComponentFinderAttribute>(this);
        }

        [ContextMenu("Find All Components")]
        private void FindAllComponents()
        {
            var behaviours = this.GetComponentsInChildren<AutoRefBehaviour>();
            foreach (var chinchillada in behaviours)
                chinchillada.FindComponents();
        }

        [ContextMenu("Find Components In Children")]
        private void FindComponentsInChildren() => this.FindComponentsCustom(SearchStrategy.InChildren);

        [ContextMenu("Find Components In parents")]
        private void FindComponentsInParents() => this.FindComponentsCustom(SearchStrategy.InParent);

        [ContextMenu("Find Components Anywhere")]
        private void FindComponentsAnywhere() => this.FindComponentsCustom(SearchStrategy.InScene);

        private void FindComponentsCustom(SearchStrategy strategy)
        {
            ComponentFinderAttribute.ApplyAttribute<ComponentFinderAttribute>(this, strategy);
        }
    }
}