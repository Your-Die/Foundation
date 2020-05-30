using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Chinchillada.Foundation.UI
{
    public class TextStyleTester : ChinchilladaBehaviour
    {
        [SerializeField] private string text;

        [SerializeField] private TextStyle style;
        
        [SerializeField, FindComponent] private TMP_Text textElement;

        
        [Button]
        private void OnValidate()
        {
            this.textElement.text = this.style.Apply(this.text);
        }
    }
}