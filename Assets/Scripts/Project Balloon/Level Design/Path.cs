using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script controlling the path of enemies
 * Written by Nikhil Ghosh '24
 */

public class Path : MonoBehaviour
{
    public List<Vector2> points;

    public void AddPoint(Vector2 newPoint)
    {
        points.Add(newPoint);
    }
}
