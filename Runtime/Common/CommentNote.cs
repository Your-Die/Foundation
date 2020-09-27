using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Foundation
{
    public class CommentNote : MonoBehaviour
    {
        [SerializeField, MultiLineProperty(8), UsedImplicitly]
        private string comment;
    }
}