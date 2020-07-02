using UnityEngine;

namespace Chinchillada.Foundation
{
    public class CameraMover : ChinchilladaBehaviour
    {
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float zoomSpeed = 1;
        
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";

        [SerializeField] private string zoomAxis = "Mouse ScrollWheel";
        
        [SerializeField, FindComponent] private Camera cam;

        private void Update()
        {
            // Read input.
            var horizontalInput = Input.GetAxis(this.horizontalAxis);
            var verticalInput = Input.GetAxis(this.verticalAxis);
            var zoomInput = Input.GetAxis(this.zoomAxis);

            // Cache time.
            var deltaTime = Time.deltaTime;
            
            // Calculate vectors.
            var movement = this.moveSpeed * deltaTime * new Vector3(horizontalInput, verticalInput);
            var zoom = this.zoomSpeed * deltaTime * zoomInput * Vector3.back;

            // Combine and apply.
            var translation = movement + zoom;
            this.cam.transform.Translate(translation);
        }
    }
}