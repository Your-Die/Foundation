namespace Chinchillada
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class BehaviourWithGizmos : ChinchilladaBehaviour
    {
        protected const string GizmoGroup = "Gizmos";
        
        [SerializeField, FoldoutGroup(GizmoGroup)]
        private GizmoMode gizmoMode;

        protected virtual void DrawGizmos()
        {
        }
        
        private void OnDrawGizmos()
        {
            if (this.gizmoMode == GizmoMode.Always) 
                this.DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (this.gizmoMode == GizmoMode.Selected) 
                this.DrawGizmos();
        }
    }

    public enum GizmoMode
    {
        Never,
        Selected,
        Always,
    }
}