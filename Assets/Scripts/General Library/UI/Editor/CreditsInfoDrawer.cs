using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WSoft.UI;

/*

[CustomPropertyDrawer(typeof(CreditsInfo))]
public class CreditsInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        if(GUI.Button(position, new GUIContent("Edit")))
        {
            Debug.Log("Hello");
        }

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
*/
