using JetBrains.Annotations;
using UnityEngine;

namespace Chinchillada
{
    public class ApplicationQuitter : MonoBehaviour
    {
        [UsedImplicitly]
        public void Quit() => Application.Quit();
    }
}