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
        /// <returns></returns>
        public static IEnumerator Fade(this CanvasGroup canvasGroup, float from, float to, float seconds)
        {
            yield return null;
            canvasGroup.alpha = from;
            float totalTime = 0;
            while (canvasGroup.alpha < to)
            {
                totalTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(from, to, totalTime/seconds);
                yield return null;
            }
            canvasGroup.alpha = to;
        }

             
    }
}
