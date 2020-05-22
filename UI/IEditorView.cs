using System;

namespace Chinchillada.Foundation.UI
{
    public interface IEditorView<T> : IPresenter<T>
    {
        bool CanEdit { get; set; }
        
        event Action Edited;
    }
}