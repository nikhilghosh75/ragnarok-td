/*
 * Allows you to control the volume through UnityEngine.UI.Slider objects and AudioParams
 * @ Jacob Shreve '?, @ Nikhil Ghosh '24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSoft.Audio
{
    [System.Serializable]
    public struct VolumeData
    {
        [Tooltip("The slider that controls the volume")]
        public Slider volumeSlider;

        public string settingName;

        [Tooltip("Information about the audio parameter to change")]
#if WSOFT_WWISE
        public WWise.AudioParam param;
#elif WSOFT_FMOD
        public FMOD.AudioParam param;
#else
        public Default.AudioParam param;
#endif

        /// <summary>
        /// Set the RTPC and PlayerPrefs value for the volume when the value on a slider has changed
        /// </summary>
        /// <param name="newValue">The new value for the RTPC</param>
        public void SetVolumeFromValue(float newValue)
        {
            Debug.Log(settingName + " set to " + 100 * newValue);
            volumeSlider.SetValueWithoutNotify(newValue);
            PlayerPrefs.SetFloat(settingName, 100 * newValue);
            Debug.Log(PlayerPrefs.GetFloat(settingName));
            // param.Set(newValue); erroneous replace temporarily
            AkSoundEngine.SetRTPCValue(settingName, 100 * newValue);
        }
    }

    public class VolumeController : MonoBehaviour
    {
        [Tooltip("A list of volumes that can be controlled")]
        public List<VolumeData> volumesToControl;

        void Start()
        {
            foreach(VolumeData volume in volumesToControl)
            {
                if (PlayerPrefs.HasKey(volume.settingName))
                {
                    volume.SetVolumeFromValue(PlayerPrefs.GetFloat(volume.settingName)/100);
                }
                else
                {
                    volume.SetVolumeFromValue(0.6f);
                }
                volume.volumeSlider.onValueChanged.AddListener(volume.SetVolumeFromValue);
            }
        }
    }
}