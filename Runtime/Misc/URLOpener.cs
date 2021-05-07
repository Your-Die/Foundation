using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada
{
    public class URLOpener : MonoBehaviour
    {
        [SerializeField] private string url;

        [UsedImplicitly]
        public void OpenURL() => Application.OpenURL(this.url);
    }
}