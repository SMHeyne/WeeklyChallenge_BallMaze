using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour
{
    public Boolean ballStop = false;
    
    void OnCollisionEnter(Collision ball) 
    {
        if (ball.gameObject.tag == "Finish")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Ball hit final area");
            ballStop = true;
        }    
    }
}
