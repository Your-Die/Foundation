namespace Chinchillada.Foundation.UI
{
    public interface IFreezableTribune
    {
        void Unsummon(object summoner);
        void ForceHide();
        void Freeze();
        void Unfreeze();
    }
}