using UnityEngine;

namespace Chinchillada
{
    public static class LayerHelper
    {
        public static LayerMask AddLayer(LayerMask layerMask, int layer) => layerMask | (1 << layer);

        public static LayerMask RemoveLayer(LayerMask layerMask, int layer) => layerMask & ~(1 << layer);
    }
}