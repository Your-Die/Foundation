using Chinchillada;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mutiny.Thesis.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent, Required]
        private Button button;

        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private TMP_Text textElement;


        public Button Button => this.button;

        public TMP_Text TextElement => this.textElement;
    }
}