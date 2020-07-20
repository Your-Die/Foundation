namespace Chinchillada.Foundation
{
    public interface IBidirectionalTweener
    {
        void TweenForward();
        void TweenBackward();
        void ForceForward();
        void ForceBackward();
    }
}