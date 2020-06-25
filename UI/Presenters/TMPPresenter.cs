using TMPro;
using UnityEngine;

namespace Chinchillada.Foundation.UI
{
    public class TMPPresenter : ChinchilladaBehaviour, IPresenter<string>
    {
        [SerializeField, FindComponent(SearchStrategy.InChildren)]
        private TMP_Text textElement;

        [SerializeField] private string style;

        public void Present(string content)
        {
            if (!string.IsNullOrWhiteSpace(this.style)) 
                content = content.WrapStyle(this.style);

            this.textElement.text = content;
        }

        public void Hide() => this.textElement.text = string.Empty;
    }
}