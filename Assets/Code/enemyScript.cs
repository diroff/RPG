using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyScript : MonoBehaviour
{
    public static int  lvlEn = 1, hpEn = hpEnmax, damEn = (lvlEn * 5), hpEnmax = 10, damag;
    public static bool item = false;
    public Text enemyTextHp;
    private void Update()
    {
        if (item == false) //� �������� ����������� �� � ������
        {
            if (hpEn <= 0)
            {
                enemyTextHp.text = "������";
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
