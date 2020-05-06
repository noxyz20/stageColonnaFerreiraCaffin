using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private float ecoule = 0f;
    float theTimer = 0.0f;
    public float theStartTime = 120.0f;
    public bool showRemaining;
    // Start is called before the first frame update
    void Start()
    {
        showRemaining = true;
        theTimer = theStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        print(ecoule);
        theTimer -= Time.deltaTime;
        ecoule += Time.deltaTime;
        //print(theTimer);
        if (theTimer < 10) 
        {
            Debug.Log("TEN SECONDS LEFT !");
            showRemaining = true;
        }
        if (theTimer <= 0) 
        {
            Debug.Log("OUT OF TIME");
            theTimer = 0;
        }
        //if ( Input.GetKeyUp(KeyCode.G) )
        //{
        //    Debug.Log("Resetting");
        //    theTimer = theStartTime;
        //    showRemaining = false;
        //}
    }
    
    void OnGUI() 
    {
        string text = string.Format( "{0:00}:{1:00}", Convert.ToInt32( theTimer / 60.0 ), Convert.ToInt32( theTimer % 60.0 ) ); 
        if (showRemaining)
        {
            GUI.Label( new Rect(600, 20, Screen.width - 20, 30), text );
        }
    }
}