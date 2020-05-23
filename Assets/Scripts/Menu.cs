using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Label( new Rect(250, 25, Screen.width - 20, 30), File.ReadAllText("score.scoreFile"));
    }


    public void Play()
    {
        PlayerPrefs.SetInt("score", 1);
        SceneManager.LoadScene("level1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}