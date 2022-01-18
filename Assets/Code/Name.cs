using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Name : MonoBehaviour
{
    public InputField nam;
    public static string ggName;
    private void Update()
    {
        ggName = nam.text;
        PlayerPrefs.SetString("PlayerName", ggName);
        PlayerPrefs.Save();
    }
    public void Start()
    {
        nam.text = PlayerPrefs.GetString("PlayerName");
    }
    public void onClick()
    {
        SceneManager.LoadScene("Lvl0");
    }
}
