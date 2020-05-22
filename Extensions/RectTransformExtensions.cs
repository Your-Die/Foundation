namespace Chinchillada.Foundation
{
    using UnityEngine;

    public static class RectTransformExtensions
    {
        public static bool ScreenPointToWorldPoint(this RectTransform transform, Camera camera, Vector3 position,
            out Vector3 point)
        {
            return RectTransformUtility.ScreenPointToWorldPointInRectangle(transform, position, camera, out point);
        }

        public static Camera GetCanvasCamera(this RectTransform transform)
        {
            var canvas = transform.GetComponentInParent<Canvas>();
            return canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        }
    }
}