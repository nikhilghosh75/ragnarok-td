/*
 * Controls the Controller Vibration using the Unity Input system
 * Note that all methods are static
 * @ Matt Rader '19, Nikhil Ghosh '23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WSoft.Input
{
    [System.Serializable]
    public struct VibrationSettings
    {
        [Tooltip("This sets the motor speed for the controller. Must be a value between 0 and 1.")]
        public float vibrationIntensity;

        [Tooltip("Length of the vibration in seconds")]
        public float vibrationDuration;
    }

    public class ControllerVibration : MonoBehaviour
    {
        /// <summary>
        /// Vibrates the current controller, if it exists
        /// </summary>
        /// <param name="settings">The settings of the vibration</param>
        public static void VibrateController(VibrationSettings settings)
        {
            Gamepad pad = Gamepad.current;
            if(pad == null)
            {
                return;
            }

            WSoft.Core.GameManager gameManager = WSoft.Core.GameManager.Instance;
            if(gameManager == null)
            {
                Debug.LogError("Game Manager does not exist in current scene");
                return;
            }
            gameManager.StartCoroutine(VibrateControllerCoroutine(pad, settings));
        }

        /// <summary>
        /// Vibrates the controller at the specified index, if it exists
        /// </summary>
        /// <param name="index">The index to vibrate the controller</param>
        /// <param name="settings">The settings of the vibration</param>
        public static void VibrateController(int index, VibrationSettings settings)
        {
            UnityEngine.InputSystem.Utilities.ReadOnlyArray<Gamepad> pads = Gamepad.all;
            if (index >= pads.Count)
            {
                Debug.LogError("Controller at index " + index + " not found.");
                return;
            }
            Gamepad currentGamepad = pads[index];

            WSoft.Core.GameManager gameManager = WSoft.Core.GameManager.Instance;
            if (gameManager == null)
            {
                Debug.LogError("Game Manager does not exist in current scene");
                return;
            }
            gameManager.StartCoroutine(VibrateControllerCoroutine(currentGamepad, settings));
        }

        /// <summary>
        /// Pause the controller vibration. Useful for pause menus or other functions where Haptics must stop.
        /// </summary>
        public static void PauseControllerVibration()
        {
            InputSystem.PauseHaptics();
        }

        /// <summary>
        /// Resumes the controller vibration if it was paused.
        /// </summary>
        public static void ResumeControllerVibration()
        {
            InputSystem.ResumeHaptics();
        }

        static IEnumerator VibrateControllerCoroutine(Gamepad pad, VibrationSettings settings)
        {
            pad.SetMotorSpeeds(settings.vibrationIntensity, settings.vibrationIntensity);
            yield return new WaitForSeconds(settings.vibrationDuration);
            pad.SetMotorSpeeds(0f, 0f);
        }
    }
}
