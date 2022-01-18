using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void onClick()
    {
        SceneManager.LoadScene(0);
    }
    public void exitClick()
    {
        Application.Quit();
    }
    public void openRecord()
    {
        SceneManager.LoadScene(3);
    }

}
