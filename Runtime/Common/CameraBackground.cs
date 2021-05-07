namespace Chinchillada.Generation.Turtle
{
    using Chinchillada;
    using Sirenix.OdinInspector;
    using Sirenix.Serialization;
    using UnityEngine;

    public class CameraBackground : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent(SearchStrategy.InScene), Required]
        private Camera cam;

        [OdinSerialize] private ISource<Color> colorschemeReference;

        private void OnEnable() => this.UpdateCamera();

        [Button]
        public void UpdateCamera()
        {
            this.cam.backgroundColor = this.colorschemeReference.Get();
        }
    }
}