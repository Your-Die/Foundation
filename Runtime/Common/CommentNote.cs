using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada
{
    public class CommentNote : MonoBehaviour
    {
        [SerializeField, MultiLineProperty(8), UsedImplicitly]
        private string comment;
    }
}