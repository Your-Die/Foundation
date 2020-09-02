using System.Collections;
using Chinchillada.Timers;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chinchillada.Foundation.Tests
{
    public static class TimerTests
    {
        private static float[] durationValues = {0, 0.1f, 0.001f, 0.0001f, 1, 3.2f};

        [UnityTest]
        public static IEnumerator StartTimer_IsFinished_AfterDuration(
            [ValueSource(nameof(durationValues))] float duration)
        {
            var timer = new Timer(duration);

            timer.Start();
            yield return new WaitForSeconds(duration);

            Assert.That(timer.IsRunning, Is.False);
        }

        [UnityTest]
        public static IEnumerator StartTimer_CallbackInvoked_AfterDuration(
            [ValueSource(nameof(durationValues))] float duration)
        {
            var isCalled = false;
            var timer = new Timer(duration, () => isCalled = true);

            timer.Start();
            yield return new WaitForSeconds(duration);

            Assert.That(isCalled, Is.True);
        }
    }
}