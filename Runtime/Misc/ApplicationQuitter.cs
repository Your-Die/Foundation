using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public class ApplicationQuitter : MonoBehaviour
    {
        [UsedImplicitly]
        public void Quit() => Application.Quit();
    }
}