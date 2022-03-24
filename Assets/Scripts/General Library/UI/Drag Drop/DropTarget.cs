using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Part of the Drag/Drop System

[System.Serializable]
public class DropEvent : UnityEvent<GameObject> { }

public class DropTarget : MonoBehaviour, IDropHandler
{
    [Tooltip("The list of types that the drop target will accept")]
    public List<string> acceptableTypes;

    public DropEvent OnDropAccepted;
    public DropEvent OnDropRejected;

    void Start()
    {
        if (acceptableTypes.Count == 0)
        {
            Debug.LogWarning("Drop Target " + gameObject.name + " does not have any acceptable types.");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableObject draggable = eventData.pointerDrag.GetComponent<DraggableObject>();
            if (draggable != null)
            {
                if (acceptableTypes.Exists(x => (x == draggable.type)))
                {
                    OnDropAccepted.Invoke(eventData.pointerDrag);
                }
                else
                {
                    OnDropRejected.Invoke(eventData.pointerDrag);
                }
            }
        }
    }
}