using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using System;

using UnityEngine;

public class LevelConfigTreeView : TreeView
{
    TreeViewItem root = null;
    LevelConfig config = null;

    Action<LevelConfig.Round> roundSelectionChanged = null;

    public LevelConfigTreeView(TreeViewState treeViewState, LevelConfig config, Action<LevelConfig.Round> roundSelectionChanged)
        : base(treeViewState)
    {
        this.config = config;
        this.roundSelectionChanged = roundSelectionChanged;
        
        rowHeight = 20;
        showAlternatingRowBackgrounds = true;
        useScrollView = true;

        Reload();
    }

    protected override bool CanMultiSelect(TreeViewItem item)
    {
        return false;
    }

    protected override TreeViewItem BuildRoot()
    {
        int id = 0;
        root = new TreeViewItem { id = id++, depth = -1, displayName = "Root" };

        var allItems = new List<TreeViewItem>();
        config.rounds.ForEach(round => 
                              allItems.Add(new TreeViewItem { id = id++,
                                                              depth = 0,
                                                              displayName = string.Format("Round {0}:      {1} waves", id - 1, config.rounds[id - 2].waves.Count)}));

        // Utility method that initializes the TreeViewItem.children and .parent for all items.
        SetupParentsAndChildrenFromDepths(root, allItems);

        // Return root of the tree
        return root;
    }

    protected override void SelectionChanged(IList<int> selectedIds)
    {
        int id = selectedIds[0] - 1;
        if (config.rounds.Count < id + 1) return;

        roundSelectionChanged?.Invoke(config.rounds[id]);
    }
}
