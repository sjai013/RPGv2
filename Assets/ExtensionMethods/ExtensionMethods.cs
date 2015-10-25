using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ExtensionMethods
{
    
    public static class ExtensionMethods
    {
        static readonly System.Random Rand = new System.Random(Guid.NewGuid().GetHashCode());

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

        /// <summary>
        /// Calculates a polynomial
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="coefficients">Coefficients in descending order (last element is 0th order)</param>
        /// <returns></returns>
        public static float Polynomial(this float x, params float[] coefficients)
        {
            float polynomial = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                polynomial += coefficients[i] * Mathf.Pow(x,coefficients.Length - i - 1);
            }
            return polynomial;
        }


        public static Boolean ToRandom(this float x)
        {
            bool result = Rand.NextDouble() < x;
            return result;
        }

        public static String PrintList<T>(this List<T> list)
        {
            var sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.Append(item.ToString()+'\n');
            }

            return sb.ToString();

        }

        public static void RemoveAllChildren(this GameObject go)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                UnityEngine.Object.Destroy(go.transform.GetChild(i).gameObject);
            }
        }

    }
}
