using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chinchillada.Foundation.UI
{
    /// <summary>
    /// Utility interface for accessing both the <see cref="Button"/> and associated <see cref="TMP_Text"/>
    /// on a button object.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonController : ChinchilladaBehaviour
    {
        /// <summary>
        /// The button.
        /// </summary>
        [SerializeField, FindComponent, Required]
        private Button button;

        /// <summary>
        /// The text.
        /// </summary>
        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private TMP_Text textElement;

        /// <summary>
        /// The button.
        /// </summary>
        public Button Button => this.button;

        /// <summary>
        /// The text.
        /// </summary>
        public TMP_Text TextElement => this.textElement;
    }
}