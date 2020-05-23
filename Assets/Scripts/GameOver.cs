using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        try
        {
            if (PlayerPrefs.GetInt("score") > int.Parse(File.ReadAllText("score.scoreFile")))
            {
                File.WriteAllText("score.scoreFile", PlayerPrefs.GetInt("score").ToString());
            }
        }
        catch (Exception e)
        {
            File.WriteAllText("score.scoreFile", PlayerPrefs.GetInt("score").ToString());
        }
        
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}