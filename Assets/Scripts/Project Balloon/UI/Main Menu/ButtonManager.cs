/*
 * A system that lets an indicator slide over the active menu button
 * Written by Andrew Zhou '22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public RectTransform indicator;

    public float indicatorSlowdown = 1.0f;
    public List<RectTransform> buttons;
    
    private List<float> btnLocations;
    private bool moving;

    private Vector2 start;
    private float startTime;
    private Vector2 end;

    private void Start() 
    {
        btnLocations = new List<float>();
        StartCoroutine(LayoutWait());
    }

    void Update()
    {
        if (moving)
        {
            float t = (Time.unscaledTime - startTime) / indicatorSlowdown;
            indicator.position = Vector2.Lerp(indicator.position, end, t);
        }
    }

    public void MoveIndicator(int btnNum)
    {
        start = indicator.position;
        startTime = Time.unscaledTime;
        end = new Vector3(btnLocations[btnNum], start.y, 0);
        //indicator.SetPositionAndRotation(end, Quaternion.identity);
        moving = true;
    }
    
    /*
     * Yes, I know this code is hamfisted and bad.
     * If you want to figure out a way to get the x values of the
     * buttons before being moved by the Horizontal Layout Group,
     * be my guest.
     * - Andrew
     */
    IEnumerator LayoutWait()
    {
        yield return new WaitForSeconds((float) 0.01);
        foreach (RectTransform btn in buttons){
            float btnX = btn.position.x;
            btnLocations.Add(btnX);
        }
        // btnLocations[x] should always be set to the "START" button
        indicator.SetPositionAndRotation(new Vector3(btnLocations[2], indicator.position.y, 0), Quaternion.identity);
        start = indicator.position;
    }
}
