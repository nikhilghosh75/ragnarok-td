/*
 * Handles the screen transition on death (fade to black, then fade back in to next screen)
 * @ Christine Chen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

namespace WSoft.UI
{
    public class DeathScreenTransition : MonoBehaviour
    {
        [Tooltip("Time it takes to fade in/out the screen in seconds")]
        public float FadeTime;

        [Tooltip("Image to fade into")]
        public CanvasRenderer FadeImage;

        public UnityEvent OnTransitionOver;

        private void Start()
        {
            FadeImage.SetAlpha(0);
        }

        /// <summary>
        /// Fades the screen out and back into death screen
        /// </summary>
        public void PlayDeathScreenTransition()
        {
            Debug.Log("Playing death screen transition animation");
            StartCoroutine(ScreenTransition());
        }

        IEnumerator ScreenTransition()
        {
            // Fade out to black screen
            float timeElapsed = 0;
            while (timeElapsed < FadeTime)
            {
                FadeImage.SetAlpha(Mathf.Lerp(0, 1, timeElapsed / FadeTime));
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            FadeImage.SetAlpha(1);
            OnTransitionOver?.Invoke();
        }
    }
}