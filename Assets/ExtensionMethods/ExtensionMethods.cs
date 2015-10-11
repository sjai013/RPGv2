using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Fade in/out a CanvasGroup object
        /// </summary>
        /// <param name="canvasGroup">CanvasGroup to fade</param>
        /// <param name="from">Starting alpha value</param>
        /// <param name="to">Final alpha value</param>
        /// <param name="seconds">Duration over which fading should occur</param>
        /// <param name="method">Method to run after finishing coroutine</param>
        /// <returns></returns>
        public static IEnumerator Fade(this CanvasGroup canvasGroup, float from, float to, float seconds, Action method = null)
        {
            yield return null;
            canvasGroup.alpha = from;
            float totalTime = 0;
            while (Mathf.Abs(canvasGroup.alpha - to) > 0.1)
            {
                totalTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(from, to, totalTime/seconds);
                yield return null;
            }
            canvasGroup.alpha = to;

            if (method == null) yield break;
            method();
        }

             
    }
}
