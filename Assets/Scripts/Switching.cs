using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour
{
    public GameObject switchPivot;
    Boolean switchLeft = true;
    
    private void OnMouseDown()
    {
        Debug.Log("Click!");
        if(switchLeft)
        {
            switchPivot.transform.Rotate(0.0f, 0.0f, 60f, Space.Self);
        }
        else
        {
            switchPivot.transform.Rotate(0.0f, 0.0f, -60f, Space.Self);
        }
        // immer umdrehen des true/ false - egal was vorher durchgelaufen ist
        switchLeft = !switchLeft;

    }
}
