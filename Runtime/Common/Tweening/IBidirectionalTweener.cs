using System;
using DG.Tweening;

namespace Chinchillada.Foundation
{
    public interface IBidirectionalTweener
    {
        void TweenForward();
        void TweenBackward();
        void TweenBackward(TweenCallback onFinished);
        void ForceForward();
        void ForceBackward();
    }
}