namespace Chinchillada.Foundation.UI
{
    public interface ITribunePresenter<T> : IFreezableTribune, IPerformer<T>
    {
        void Summon(object summoner, int priority, T content);
    }
}