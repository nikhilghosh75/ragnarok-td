using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEditor.IMGUI.Controls;
using UnityEditor.TreeViewExamples;

public class LevelConfigEditor : EditorWindow
{
    [MenuItem("Project Balloon/Level Config Editor")]
    public static void OpenWindow()
    {
        GetWindow<LevelConfigEditor>(false, "Level Config Editor", true);
    }

    private void OnGUI()
    {
        EditExisting();
    }

    bool isLoaded = false;

    LevelConfig editConfig;
    Rect headerRect;

    LevelConfigTreeView levelTreeView;
    TreeViewState levelTreeState;


    RoundView roundView;
    TreeViewState roundViewState;
    MultiColumnHeaderState roundMultiColumnHeaderState;

    float treeWidth = 200f;

    //EditorGUISplitView horizontalSplitView = new EditorGUISplitView(EditorGUISplitView.Direction.Horizontal);

    void EditExisting()
    {
        // Config
        headerRect = EditorGUILayout.BeginHorizontal();
        editConfig = (LevelConfig)EditorGUILayout.ObjectField("Original Level Config", editConfig, typeof(LevelConfig), false);
        if (GUILayout.Button("Load Level Config Data"))
        {
            if (editConfig == null) return;
            if (levelTreeState == null) levelTreeState = new TreeViewState();

            levelTreeView = new LevelConfigTreeView(levelTreeState, editConfig, OnRoundSelectionChanged);
            isLoaded = true;
        }
        EditorGUILayout.EndHorizontal();

        if (!isLoaded) return;

        EditorGUILayout.BeginHorizontal();
        //horizontalSplitView.BeginSplitView();

        LevelTreeViewGUI();

        //horizontalSplitView.Split();

        RoundViewGUI();

        //horizontalSplitView.EndSplitView();
        EditorGUILayout.EndHorizontal();

    }

    void LevelTreeViewGUI()
    {
        // left side: round list in a level
        if (levelTreeView == null) return;

        EditorGUILayout.BeginVertical();

        float treeY = 5f + headerRect.y + headerRect.height;
        levelTreeView.OnGUI(new Rect(0, treeY, treeWidth, position.height - treeY));

        EditorGUILayout.EndVertical();
    }

    void OnRoundSelectionChanged(LevelConfig.Round round)
    {
        if (editConfig == null || round == null) return;
        if (roundViewState == null) roundViewState = new TreeViewState();

        InitHeaderState();
        var treeModel = new TreeModel<RoundTreeElement>(GetRoundData(round));
        roundView = new RoundView(roundViewState, new MultiColumnHeader(roundMultiColumnHeaderState), treeModel);
    }

    void InitHeaderState()
    {
        var headerState = RoundView.CreateDefaultMultiColumnHeaderState(position.width - treeWidth);
        if (MultiColumnHeaderState.CanOverwriteSerializedFields(roundMultiColumnHeaderState, headerState))
            MultiColumnHeaderState.OverwriteSerializedFields(roundMultiColumnHeaderState, headerState);
        roundMultiColumnHeaderState = headerState;
    }

    IList<RoundTreeElement> GetRoundData(LevelConfig.Round round)
    {
        List<RoundTreeElement> dataList = new List<RoundTreeElement>();
        dataList.Add(new RoundTreeElement(null) { id = 0, depth = -1 });
        round.waves.ForEach(wave => dataList.Add(new RoundTreeElement(wave)));

        return dataList;
    }

    void RoundViewGUI()
    {
        if (roundView == null) return;

        EditorGUILayout.BeginVertical();

        Rect headerRect = RoundHeaderGUI();
        float treeY = headerRect.y + headerRect.height;

        roundView.OnGUI(new Rect(5f + treeWidth, treeY, position.width - treeWidth - 5f, position.height - treeY));

        EditorGUILayout.EndVertical();
    }

    Rect RoundHeaderGUI()
    {
        Rect rect = EditorGUILayout.BeginVertical();



        EditorGUILayout.EndVertical();
        return rect;
    }
}
