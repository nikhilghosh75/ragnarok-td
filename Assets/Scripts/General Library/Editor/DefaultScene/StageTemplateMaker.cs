/*
 * Creates a tool menu item and logic for making a new stage and transition in a zone
 * @Zena Abulhab
 */

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace WSoft.Tools.DefaultScene
{
    public static class StageTemplateMaker
    {
        // Add menu item
        [MenuItem("Tools/Add New Stage", false, 100)]
        static void DoSomething(MenuCommand command)
        {
            Scene scene = EditorSceneManager.GetActiveScene();
            GameObject zone = GameObject.FindGameObjectWithTag("Zone");
            // instatiate stage prefab with zone as parent
            GameObject newStage = PrefabUtility.InstantiatePrefab((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath(
                "Assets/Prefabs/Levels/Templates/Stage Template.prefab", typeof(GameObject)), zone.transform) as GameObject;
            newStage.name = "NewStage";
            PrefabUtility.UnpackPrefabInstance(newStage, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

            GameObject newTransition = PrefabUtility.InstantiatePrefab((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath(
                "Assets/Prefabs/Levels/Templates/TransitionPoint Template.prefab", typeof(GameObject)), zone.transform) as GameObject;
            newTransition.name = "NewTransition";
            PrefabUtility.UnpackPrefabInstance(newTransition, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }
    }
}

#endif