using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/*
 * A way to change post processing by UnityEvents
 * Written by Nikhil Ghosh '24
 */

namespace WSoft.VFX
{
    public class PostProcessEvents : MonoBehaviour
    {
        #region BLOOM

        [System.Serializable]
        public struct BloomSettings
        {
            public float intensity;
            public float threshold;
            public Color color;
        }

        public bool bloomEnabled;
        public BloomSettings bloomSettings;

        #endregion
        #region CHROMATIC_ABBERATION

        [System.Serializable]
        public struct ChromaticAbberationSettings
        {
            public float intensity;
            public bool fastMode;
        }

        public bool chromaticAbberationEnabled;
        public ChromaticAbberationSettings chromaticAbberationSettings;

        #endregion
        #region DEPTH_OF_FIELD

        [System.Serializable]
        public struct DepthOfFieldSettings
        {
            public float focusDistance;
            public float aperture;
            public float focalLength;
        }

        public bool depthOfFieldEnabled;
        public DepthOfFieldSettings depthOfFieldSettings;

        #endregion
        #region GRAIN

        [System.Serializable]
        public struct GrainSettings
        {
            public bool colored;
            public float intensity;
            public float size;
            public float luminanceContribution;
        }

        public bool grainEnabled;
        public GrainSettings grainSettings;

        #endregion
        #region LENS_DISTORTION

        [System.Serializable]
        public struct LensDistortionSettings
        {
            public float intensity;
            public float yMultiplier;
            public float xMultiplier;
            public float yCenter;
            public float xCenter;
        }

        public bool lensDistortionEnabled;
        public LensDistortionSettings lensDistortionSettings;

        #endregion
        #region MOTION_BLUR

        [System.Serializable]
        public struct MotionBlurSettings
        {
            public float shutterAngle;
            public float sampleCount;
        }

        public bool motionBlurEnabled;
        public MotionBlurSettings motionBlurSettings;

        #endregion

        public string layer;
        public float priority;
        PostProcessVolume postProcessVolume;

        public void CreateVolume()
        {
            Bloom bloom = ScriptableObject.CreateInstance<Bloom>();

            int layerInt = LayerMask.NameToLayer(layer);
            postProcessVolume = PostProcessManager.instance.QuickVolume(layerInt, priority, bloom);
        }

        public int NumPostProcessingEffects()
        {
            int numPostProcessingEffects = 0;

            if (bloomEnabled)
                numPostProcessingEffects++;

            if (chromaticAbberationEnabled)
                numPostProcessingEffects++;

            if (depthOfFieldEnabled)
                numPostProcessingEffects++;

            if (grainEnabled)
                numPostProcessingEffects++;

            if (lensDistortionEnabled)
                numPostProcessingEffects++;

            if (motionBlurEnabled)
                numPostProcessingEffects++;

            return numPostProcessingEffects;
        }
    }
}