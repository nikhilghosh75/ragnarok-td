using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.TreeViewExamples;

[Serializable]
public class RoundTreeElement : TreeElement
{
    public Texture thumbnail;
    public GameObject enemyPrefab;
    public int enemyAmount;
    public float timeBetweenEnemies;
    public int pathIndex;

    public RoundTreeElement(LevelConfig.Wave wave)
    {
        if (wave == null) return;

        enemyPrefab = wave.enemyPrefab;
        if (enemyPrefab != null)
            thumbnail = AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(enemyPrefab));

        enemyAmount = wave.amount;
        timeBetweenEnemies = wave.timeBetweenEnemies;
        pathIndex = wave.pathIndex;
    }
}

public class RoundView : TreeViewWithTreeModel<RoundTreeElement>
{
    enum ColumnNames
    {
        id,
        thumbnail,
        enemyPrefab,
        enemyAmount,
        timeBetweenEnemies,
        pathIndex
    }

    public RoundView(TreeViewState treeViewState, MultiColumnHeader header, TreeModel<RoundTreeElement> model)
        : base(treeViewState, header, model)
    {
        rowHeight = 20;
        showAlternatingRowBackgrounds = true;
        useScrollView = true;

        Reload();
    }

    protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
    {
        var rows = base.BuildRows(root);
        return rows;
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        var item = (TreeViewItem<RoundTreeElement>)args.item;

        for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
        {
            CellGUI(args.GetCellRect(i), item, (ColumnNames)args.GetColumn(i), ref args);
        }
    }

    void CellGUI(Rect cellRect, TreeViewItem<RoundTreeElement> item, ColumnNames column, ref RowGUIArgs args)
    {
        CenterRectUsingSingleLineHeight(ref cellRect);

        switch (column)
        {
            case ColumnNames.id:
                EditorGUI.LabelField(cellRect, item.data.id.ToString());
                break;
            case ColumnNames.thumbnail:
                GUI.DrawTexture(cellRect, item.data.thumbnail, ScaleMode.ScaleToFit);
                break;
            case ColumnNames.enemyPrefab:
                item.data.enemyPrefab = 
                    EditorGUI.ObjectField(cellRect, item.data.enemyPrefab, typeof(GameObject), false) as GameObject;
                break;
            case ColumnNames.enemyAmount:
                item.data.enemyAmount = EditorGUI.IntField(cellRect, item.data.enemyAmount);
                break;
            case ColumnNames.timeBetweenEnemies:
                item.data.timeBetweenEnemies = EditorGUI.FloatField(cellRect, item.data.timeBetweenEnemies);
                break;
            case ColumnNames.pathIndex:
                item.data.pathIndex = EditorGUI.IntField(cellRect, item.data.pathIndex);
                break;
        }
    }

    public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
    {
        var columns = new[]
        {
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Wave id", "Id of this wave"),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Right,
                width = 60,
                minWidth = 60,
                maxWidth = 60,
                autoResize = false,
                allowToggleVisibility = true
            },
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Enemy thumbnail", "A snap shot of how enemy look likes."),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Right,
                width = 130,
                minWidth = 130,
                maxWidth = 130,
                autoResize = false,
                allowToggleVisibility = true
            },
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Enemy prefab", "Predetermined prefab for this enemy."),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Right,
                width = 150,
                minWidth = 150,
                maxWidth = 200,
                autoResize = false,
                allowToggleVisibility = true
            },
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Enemy Amount", "Amount of enemy in this wave."),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Left,
                width = 60,
                minWidth = 60,
                maxWidth = 100,
                autoResize = true
            },
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Time Between Enemies", "Time between enemy ocurrence."),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Left,
                width = 160,
                minWidth = 160,
                maxWidth = 200,
                autoResize = true
            },

            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Path Index", "Index of path chosen in this map."),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Left,
                width = 100,
                minWidth = 100,
                autoResize = true
            }
        };

        //Assert.AreEqual(columns.Length, Enum.GetValues(typeof(ColumnNames)).Length, "Number of columns should match number of enum values: You probably forgot to update one of them.");

        var state = new MultiColumnHeaderState(columns);
        return state;
    }
}
