using Chinchillada.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada
{
    using System;

    /// <summary>
    /// Base class for Monobehaviours. Inherits from <see cref="Sirenix.OdinInspector.SerializedMonoBehaviour"/>.
    /// Automatically applies <see cref="FindComponentAttribute"/> on awake, and also extends a Button to manually trigger it from the Unity editor.
    /// </summary>
    public abstract class ChinchilladaBehaviour : SerializedMonoBehaviour, IComponent
    {
        private bool hasStarted;
        
        protected virtual void Awake()
        {
            this.FindComponents();
        }

        protected virtual void Start()
        {
            this.OnActivate();
            this.hasStarted = true;
        }

        protected virtual void OnEnable()
        {
            if (this.hasStarted) 
                this.OnActivate();
        }

        protected virtual void OnActivate()
        {
        }
        
        /// <summary>
        /// Applies the <see cref="FindComponentAttribute"/> on this <see cref="UnityEngine.MonoBehaviour"/>
        /// </summary>
        [Button]
        protected virtual void FindComponents()
        {
            AttributeHelper.ApplyAttribute<FindComponentAttribute>(this);
        }

        [ContextMenu("Find All Components")]
        private void FindAllComponents()
        {
            var behaviours = this.GetComponentsInChildren<ChinchilladaBehaviour>();
            foreach (var chinchillada in behaviours) 
                chinchillada.FindComponents();
        }
    }
}
