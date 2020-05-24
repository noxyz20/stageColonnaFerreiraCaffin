using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void OnGUI ()
    {
        if (File.Exists("score.scoreFile"))
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.button); //create a new variable
            guiStyle.fontSize = 40; //change the font size
            //guiStyle.fontStyle = FontStyle.Bold;
            GUI.Label( new Rect(0, Screen.height/2.25f, 0.8f*Screen.width, 60), "HighScore : "+File.ReadAllText("score.scoreFile"), guiStyle);
        
            if (GUI.Button(
                new Rect(0.8f * Screen.width, Screen.height / 2.25f, 0.2f * Screen.width, 60),
                "Reset",
                guiStyle))
            {
                File.Delete("score.scoreFile");
            }  
        }
        
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