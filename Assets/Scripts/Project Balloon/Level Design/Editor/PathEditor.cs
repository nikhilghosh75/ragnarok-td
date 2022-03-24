using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    Path path;
    List<int> selectedPoints;

    float pointRadius = 0.4f;

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        switch (guiEvent.type)
        {
            case EventType.MouseDown:
                if (guiEvent.shift)
                {
                    int mousePoint = MouseOverPoint(mousePos);
                    if (mousePoint != -1)
                    {
                        selectedPoints.Add(mousePoint);
                    }
                    else
                    {
                        selectedPoints.Clear();
                    }
                }
                else if (guiEvent.control)
                {
                    Undo.RecordObject(path, "Add Point");
                    path.AddPoint(mousePos);
                    selectedPoints.Clear();
                }
                else
                {
                    selectedPoints.Clear();
                }
                break;
            case EventType.KeyDown:
                if (guiEvent.keyCode == KeyCode.Backspace)
                {
                    DeletePoints();
                }
                break;
        }
    }

    void Draw()
    {
        for (int i = 0; i < path.points.Count; i++)
        {
            Handles.color = Color.red;
            if (IsPointSelected(i))
            {
                Handles.color = Color.yellow;
            }
            Vector2 newPosition = Handles.FreeMoveHandle(path.points[i], Quaternion.identity, pointRadius, Vector2.zero, Handles.CylinderHandleCap);
            Handles.Label(path.points[i], new GUIContent(i.ToString()));
            if (Vector2.SqrMagnitude(newPosition - path.points[i]) > 0.01f)
            {
                Undo.RecordObject(path, "Move Point");
                path.points[i] = newPosition;
            }

            if(i != path.points.Count - 1)
            {
                Handles.color = new Color(0, 1f, 0);
                Handles.DrawLine(path.points[i], path.points[i + 1], 1);
            }
        }
    }

    void OnEnable()
    {
        path = (Path)target;
        if (path.points == null)
        {
            path.points = new List<Vector2>();
            path.AddPoint(Vector2.zero);
        }
        else if (path.points.Count == 0)
        {
            path.AddPoint(Vector2.zero);
        }
        selectedPoints = new List<int>();
    }

    int MouseOverPoint(Vector2 mousePos)
    {
        for (int i = 0; i < path.points.Count; i++)
        {
            if (Vector2.SqrMagnitude(mousePos - path.points[i]) < pointRadius * pointRadius)
            {
                return i;
            }
        }
        return -1;
    }

    bool IsPointSelected(int pointIndex)
    {
        for (int i = 0; i < selectedPoints.Count; i++)
        {
            if (selectedPoints[i] == pointIndex)
            {
                return true;
            }
        }
        return false;
    }

    void DeletePoints()
    {
        selectedPoints.Sort();
        for (int i = selectedPoints.Count - 1; i >= 0; i--)
        {
            path.points.RemoveAt(selectedPoints[i]);
        }
        selectedPoints.Clear();
    }
}
