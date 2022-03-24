using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptScrollController : MonoBehaviour
{

    private float currentYValue;
    public float endYValue;
    public float speed = ((float)0.5);

    // Start is called before the first frame update
    void Start()
    {
        currentYValue = this.GetComponent<RectTransform>().localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentYValue < endYValue)
        {
            currentYValue = currentYValue + speed;
            this.GetComponent<RectTransform>().localPosition = new Vector3(0,currentYValue,0);
        }
    }
}
