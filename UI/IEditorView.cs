using System;

namespace Mutiny.Foundation.UI
{
    public interface IEditorView<T> : IPresenter<T>
    {
        bool CanEdit { get; set; }
        
        event Action Edited;
    }
}