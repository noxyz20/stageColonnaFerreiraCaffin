using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonAudio : MonoBehaviour
{
    private static SingletonAudio instance = null;
    public static SingletonAudio Instance {
        get { return instance; }
    }
    void Awake() 
    {
        if (instance != null && instance != this) 
        {
            if(instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
            {
                instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
                instance.GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume;
                instance.GetComponent<AudioSource>().Play();
            }

            Destroy(this.gameObject);
            return;
        } 
        instance = this;
        GetComponent<AudioSource>().Play ();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetSceneAt(0).name=="die")
        {
            print("!!!!!");
            Destroy(this.gameObject);
        }
    }
}
