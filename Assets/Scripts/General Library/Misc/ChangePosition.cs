using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    public Vector3 pos;
   public void SetPosToMyPos() {
       transform.position = pos;
   }
}
