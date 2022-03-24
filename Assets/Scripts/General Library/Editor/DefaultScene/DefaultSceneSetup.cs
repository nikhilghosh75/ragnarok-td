/*
 * Defines what gameobjects go into a new scene when created.
 * @Zena Abulhab
 */

#if UNITY_EDITOR

using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace WSoft.Tools.DefaultScene
{
    [UnityEditor.InitializeOnLoad]
    public static class DefaultSceneSetup
    {

        // When Unity editor opens, this constructor sets up the event callback
        static DefaultSceneSetup()
        {
            EditorSceneManager.newSceneCreated += SetUpScene;
        }

        private static void SetUpScene(Scene scene, NewSceneSetup setup, NewSceneMode mode)
        {
            // destroy new scene default camera
            Object.DestroyImmediate(UnityEngine.Camera.main.gameObject);

            GameObject go = UnityEditor.PrefabUtility.InstantiatePrefab((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/_Development/ToolAssets/DefaultScene.prefab", typeof(GameObject))) as GameObject;
            UnityEditor.PrefabUtility.UnpackPrefabInstance(go, UnityEditor.PrefabUnpackMode.OutermostRoot, UnityEditor.InteractionMode.AutomatedAction);
            go.transform.DetachChildren();
            Object.DestroyImmediate(go);
        }
    }
}
#endif