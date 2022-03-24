using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * A component designed to limit when a tower can be dropped
 * Written by Nikhil Ghosh '24
 */

public class NonDroppableRect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool pointerOver = false;

    void OnDisable()
    {
        // When component is disabled, treat it as if the mouse has left it
        if(pointerOver)
        {
            OnPointerExit(null);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TowerDropper.nonDroppableRectsOverlapping++;
        // Debug.Log("Non Droppable Region Entered" + gameObject.name);

        pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TowerDropper.nonDroppableRectsOverlapping--;
        pointerOver = false;
        // Debug.Log("Non Droppable Region Exited" + gameObject.name);

        if (TowerDropper.nonDroppableRectsOverlapping < 0)
        {
            TowerDropper.nonDroppableRectsOverlapping = 0;
        }
    }
}
