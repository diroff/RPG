using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Record : MonoBehaviour
{
    public Text rec1, rec2, rec3, rec4, rec5;

    private void Update()
    {
        rec1.text = PlayerPrefs.GetString("Place1Str") + " : " + PlayerPrefs.GetInt("Place1").ToString();
        rec2.text = PlayerPrefs.GetString("Place2Str") + " : " + PlayerPrefs.GetInt("Place2").ToString();
        rec3.text = PlayerPrefs.GetString("Place3Str") + " : " + PlayerPrefs.GetInt("Place3").ToString();
        rec4.text = PlayerPrefs.GetString("Place4Str") + " : " + PlayerPrefs.GetInt("Place4").ToString();
        rec5.text = PlayerPrefs.GetString("Place5Str") + " : " + PlayerPrefs.GetInt("Place5").ToString();
    }
}
