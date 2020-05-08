using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateLevel2 : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        SceneManager.LoadScene("level1");
    }
}
