using JetBrains.Annotations;
using UnityEngine;

namespace Mutiny.Foundation
{
    public class URLOpener : MonoBehaviour
    {
        [SerializeField] private string url;

        [UsedImplicitly]
        public void OpenURL() => Application.OpenURL(this.url);
    }
}