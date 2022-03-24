using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * A class that moves enemies throughout the level
 * Written by Nikhil Ghosh '24
 */

public class EnemyMovement : MonoBehaviour
{
    public float speed;

    public bool rotateInMovement = false;

    [HideInInspector]
    public Path path;

    public UnityEvent OnPathEnd;

    public int currentPathPoint = 0;
    float currentPositionOnPath = 0;

    // Setup the view transform in child
    float lastSpeed;
    Transform view = null;
    IEnumerator stunHandler;
    // Start is called before the first frame update
    void Start()
    {
        view = transform.Find("View");
        lastSpeed = speed;
    }

    bool rotationNeededInThisFrame = false;
    // Update is called once per frame
    void Update()
    {
        if (currentPathPoint + 1 >= path.points.Count)
        {
            OnEndOfPath();
            return;
        }

        Vector2 nextPoint = path.points[currentPathPoint + 1];
        Vector2 direction = (nextPoint - (Vector2)transform.position).normalized;
        float movementDist = speed * Time.deltaTime;
        float sqrDistToNextPoint = Vector2.SqrMagnitude(nextPoint - (Vector2)transform.position);

        if (rotateInMovement && rotationNeededInThisFrame)
        {
            // For purpose of reducing call of PerformRotation
            PerformRotation(direction);
            rotationNeededInThisFrame = false;
        }

        if(movementDist * movementDist > sqrDistToNextPoint)
        {
            transform.position = path.points[currentPathPoint + 1];
            currentPositionOnPath += Mathf.Sqrt(currentPositionOnPath);
            currentPathPoint++;

            // Perform rotation next frame
            rotationNeededInThisFrame = true;
        }
        else
        {
            transform.Translate(direction * movementDist);
            currentPositionOnPath += movementDist;
        }
    }

    void PerformRotation(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Rotate only on visual component
        view.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public float GetCurrentPositionOnPath() { return currentPositionOnPath; }

    void OnEndOfPath()
    {
        OnPathEnd.Invoke();
        Destroy(gameObject);
    }

    public void StunEnemy(float stunTime)
    {
        if (stunHandler != null)
        {
            StopCoroutine(stunHandler);
            speed = lastSpeed;
        }
        stunHandler = DoStunEnemy(stunTime);
        StartCoroutine(stunHandler);
    }

    IEnumerator DoStunEnemy(float stunTime)
    {
        lastSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(stunTime);
        speed = lastSpeed;
    }
}
