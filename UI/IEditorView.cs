namespace Mutiny.Foundation.UI
{
    public interface IEditorView<out T>
    {
        T Publish();
    }
}