using System;
using UnityEngine;


public class timer : MonoBehaviour
{
    float theTimer = 0.0f;
    public float theStartTime = 120.0f;
    public bool showRemaining;

    private int lastScore;

    private float score = 0f;
    // Start is called before the first frame update

    private void OnEnable()
    {
        lastScore  =  PlayerPrefs.GetInt("score");
        //print("SCORE : "+ lastScore);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("score", Convert.ToInt32(10*score));
        //print("SCORE Save : "+ Convert.ToInt32(10*score));
    }

    void Start()
    {
        showRemaining = true;
        theTimer = theStartTime;
        int lastScore  =  PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
    {
        //print(ecoule);
        theTimer -= Time.deltaTime;

        if (theTimer>0)
        {
            score += Time.deltaTime;
        }
        //print(theTimer);
        if (theTimer <= 0)
        {
            Debug.Log("OUT OF TIME");
            theTimer = 0;
            Destroy(GetComponentInChildren<SpriteRenderer>());
            Destroy(GetComponentInChildren<BoxCollider2D>());
        }
    }
    
    void OnGUI() 
    {
        string text = string.Format( "Encore {0:00}:{1:00}", Convert.ToInt32( theTimer / 60.0 ), Convert.ToInt32( theTimer % 60.0 ) );
        string score = string.Format("Score : {0}", lastScore + Convert.ToInt32(10*this.score));
        if (showRemaining)
        {
            GUI.Label( new Rect(0, 30, Screen.width - 20, 30), text );
            GUI.Label( new Rect(0, 40, Screen.width - 20, 30), score );
        }
    }
}