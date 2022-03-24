#if UNITY_EDITOR

/*
 * An incomplete script that should never be used
 * Written by Nikhil Ghosh '24
 */

using UnityEngine;
using UnityEditor;
using WSoft.VFX;

[CustomEditor(typeof(PostProcessEvents))]
public class PostProcessEventsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PostProcessEvents postProcess = (PostProcessEvents)target;

        // Render Bloom
        postProcess.bloomEnabled = EditorGUILayout.BeginToggleGroup("Bloom", postProcess.bloomEnabled);
        postProcess.bloomSettings.color = EditorGUILayout.ColorField("Color", postProcess.bloomSettings.color);
        postProcess.bloomSettings.intensity = EditorGUILayout.FloatField("Intensity", postProcess.bloomSettings.intensity);
        EditorGUILayout.EndToggleGroup();

        // Render Chromatic Abberation
        postProcess.chromaticAbberationEnabled = EditorGUILayout.BeginToggleGroup("Chromatic Abberation", postProcess.chromaticAbberationEnabled);
        postProcess.chromaticAbberationSettings.intensity = EditorGUILayout.FloatField("Intensity", postProcess.chromaticAbberationSettings.intensity);
        postProcess.chromaticAbberationSettings.fastMode = EditorGUILayout.Toggle("Fast Mode", postProcess.chromaticAbberationSettings.fastMode);
        EditorGUILayout.EndToggleGroup();

        // Render Depth of Field
        // Render Grain
        // Render Lens Distortion
        // Render Motion Blur
    }
}

#endif