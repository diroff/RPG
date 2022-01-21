using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text s2core, bestScore;
    public int PromScore = 0, PromScore2 = 0;
    public string PromScoreStr, PromScoreStr2;

    void Start()
    {
        s2core.text = "Ваш счёт:" + attackScript.Score + " очков";
        if (attackScript.Score > PlayerPrefs.GetInt("Place1"))
        {
            PromScore = PlayerPrefs.GetInt("Place1"); // предыдущее 1 место записывается в PromScore
            PromScoreStr = PlayerPrefs.GetString("Place1Str");
            /////////////////////////////////////////////////////////////////////////////////////////////////
            PlayerPrefs.SetInt("Place1", attackScript.Score); // текущее 1 место заполняется
            PlayerPrefs.SetString("Place1Str", Name.ggName);
            //////////////////////////////////////////////////////////////////////////////////////////
            PromScore2 = PlayerPrefs.GetInt("Place2"); //2 место записывается в PromScore2
            PromScoreStr2 = PlayerPrefs.GetString("Place2Str");

            PlayerPrefs.SetInt("Place2", PromScore); //значение Promscore передается во 2 место
            PlayerPrefs.SetString("Place2Str", PromScoreStr);

            PromScore = PlayerPrefs.GetInt("Place3"); //значение 3 места записывается в PromScore
            PromScoreStr = PlayerPrefs.GetString("Place3Str");

            PlayerPrefs.SetInt("Place3", PromScore2); //PromScore 2 записывается в 3 место
            PlayerPrefs.SetString("Place3Str", PromScoreStr2);

            PromScore2 = PlayerPrefs.GetInt("Place4"); //значение 4 места записывается в Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", PromScore); //PromScore записывается на 4 место
            PlayerPrefs.SetString("Place4Str", PromScoreStr);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 записывается на 5 место
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }
        else if ((attackScript.Score > PlayerPrefs.GetInt("Place2"))) 
        {
            PromScore2 = PlayerPrefs.GetInt("Place2"); //2 место записывается в PromScore2
            PromScoreStr2 = PlayerPrefs.GetString("Place2Str");

            PlayerPrefs.SetInt("Place2", attackScript.Score); //значение playerScript.xp передается во 2 место'
            PlayerPrefs.SetString("Place2Str", Name.ggName);

            PromScore = PlayerPrefs.GetInt("Place3"); //значение 3 места записывается в PromScore
            PromScoreStr = PlayerPrefs.GetString("Place3Str");

            PlayerPrefs.SetInt("Place3", PromScore2); //PromScore 2 записывается в 3 место
            PlayerPrefs.SetString("Place3Str", PromScoreStr2);

            PromScore2 = PlayerPrefs.GetInt("Place4"); //значение 4 места записывается в Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", PromScore); //PromScore записывается на 4 место
            PlayerPrefs.SetString("Place4Str", PromScoreStr);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 записывается на 5 место
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }
        else if ((attackScript.Score > PlayerPrefs.GetInt("Place3")))
        {
            PromScore = PlayerPrefs.GetInt("Place3"); //значение 3 места записывается в PromScore
            PromScoreStr = PlayerPrefs.GetString("Place3Str");

            PlayerPrefs.SetInt("Place3", attackScript.Score); //playerScript.xp записывается в 3 место
            PlayerPrefs.SetString("Place3Str", Name.ggName);

            PromScore2 = PlayerPrefs.GetInt("Place4"); //значение 4 места записывается в Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", PromScore); //PromScore записывается на 4 место
            PlayerPrefs.SetString("Place4Str", PromScoreStr);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 записывается на 5 место
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }


        else if ((attackScript.Score > PlayerPrefs.GetInt("Place4")))
        {
            PromScore2 = PlayerPrefs.GetInt("Place4"); //значение playerScript.xp места записывается в Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", attackScript.Score); //PromScore записывается на 4 место
            PlayerPrefs.SetString("Place4Str", Name.ggName);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 записывается на 5 место
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }
        else if ((attackScript.Score > PlayerPrefs.GetInt("Place5")))
        {
            PlayerPrefs.SetInt("Place5", attackScript.Score);
            PlayerPrefs.SetString("Place5Str", Name.ggName);
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
        bestScore.text = "Рекорд:" + PlayerPrefs.GetString("Place1Str") + ":"+ PlayerPrefs.GetInt("Place1").ToString();
        
    }
    public void onCLickExit()
    {
        Application.Quit();
    }
}
