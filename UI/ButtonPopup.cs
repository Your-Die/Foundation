using Chinchillada.Utilities;
using Mutiny.Thesis.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Mutiny.Grammar
{
    public class ButtonPopup : Popup
    {
        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private Button button;

        public Button Button => this.button;
    }
}