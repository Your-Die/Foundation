using System;
using System.Collections;
using Chinchillada;
using UnityEngine;

namespace Chinchillada.Timers
{
    public class Timing : SingletonBehaviour<Timing>
    {
        public static IEnumerator InvokeDelayed(Action action, float delay)
        {
            var routine = DelayedInvokeRoutine(action, delay);
            Instance.StartCoroutine(routine);

            return routine;
        }

        public static void CancelInvocation(IEnumerator routine)
        {
            if (routine != null)
                Instance.StopCoroutine(routine);
        }

        public static IEnumerator DelayedInvokeRoutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}