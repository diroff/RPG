using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Regen : MonoBehaviour
{
    public static Button hill;
    public void onClickRegen()
    {
        attackScript.hpLast = (int)playerScript.hp;
        playerScript.hp = playerScript.hpMax;
        playerScript.hilka -= 1;
        GetComponent<attackScript>().clickTextPool[0].StartMotionHp(playerScript.hpMax - attackScript.hpLast);
        GetComponent<attackScript>().dam.text = "Вы восстановили своё здоровье";
        if (enemyScript.hpEn >= 0)
        {
            StartCoroutine(GetComponent<attackScript>().Defense());
        }
        else if (enemyScript.hpEn <= 0 && playerScript.hilka >0)
        {
            hill.gameObject.SetActive(true);
            StartCoroutine(GetComponent<attackScript>().Attack());
        }
    }
}
