using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyScript : MonoBehaviour
{
    public static int  lvlEn = 1;
    public static float hpEn = hpEnmax, damEn = (lvlEn * 5), hpEnmax = 10;
    public static bool item = false;
    public Text enemyTextHp;
    private void Start()
    {
       
    }
    private void Update()
    {
        if (item == false) //у предмета отсутствует хп и смерть
        {
            if (hpEn <= 0)
            {
                enemyTextHp.text = "Смерть";

            }
            else
            {
                enemyTextHp.text = enemyScript.hpEn.ToString() + " hp";
            }
        }
        else
        {
            enemyTextHp.text = "";
        }
    }
}
