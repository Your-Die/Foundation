using JetBrains.Annotations;
using UnityEngine;

namespace Mutiny.Foundation
{
    public class ApplicationQuitter : MonoBehaviour
    {
        [UsedImplicitly]
        public void Quit() => Application.Quit();
    }
}