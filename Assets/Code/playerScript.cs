using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerScript : MonoBehaviour
{
    public static int  lvl = 1,  dam = (10 * lvl)+damBonus+damWea,damWea = 0,   money = 0, damBonus = 0, armor = 0, hilka = 0 ;
    public static float hpMax = (20 * lvl) + armor, hp = 20, lvlNext = 10, xp = 0;
    private void Update()
    {
        hpMax = (20 * lvl) + armor;
        dam = (10 * lvl) + damBonus + damWea;
        if (hp <= 0)
        {
            hp = 0;
            SceneManager.LoadScene(2);
        }
    }
}
