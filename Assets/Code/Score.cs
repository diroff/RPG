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
        s2core.text = "��� ����:" + playerScript.xp + " �����";
        if (playerScript.xp > PlayerPrefs.GetInt("Place1"))
        {
            PromScore = PlayerPrefs.GetInt("Place1"); // ���������� 1 ����� ������������ � PromScore
            PromScoreStr = PlayerPrefs.GetString("Place1Str");
            /////////////////////////////////////////////////////////////////////////////////////////////////
            PlayerPrefs.SetInt("Place1", (int)playerScript.xp); // ������� 1 ����� �����������
            PlayerPrefs.SetString("Place1Str", Name.ggName);
            //////////////////////////////////////////////////////////////////////////////////////////
            PromScore2 = PlayerPrefs.GetInt("Place2"); //2 ����� ������������ � PromScore2
            PromScoreStr2 = PlayerPrefs.GetString("Place2Str");

            PlayerPrefs.SetInt("Place2", PromScore); //�������� Promscore ���������� �� 2 �����
            PlayerPrefs.SetString("Place2Str", PromScoreStr);

            PromScore = PlayerPrefs.GetInt("Place3"); //�������� 3 ����� ������������ � PromScore
            PromScoreStr = PlayerPrefs.GetString("Place3Str");

            PlayerPrefs.SetInt("Place3", PromScore2); //PromScore 2 ������������ � 3 �����
            PlayerPrefs.SetString("Place3Str", PromScoreStr2);

            PromScore2 = PlayerPrefs.GetInt("Place4"); //�������� 4 ����� ������������ � Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", PromScore); //PromScore ������������ �� 4 �����
            PlayerPrefs.SetString("Place4Str", PromScoreStr);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 ������������ �� 5 �����
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }
        else if ((playerScript.xp > PlayerPrefs.GetInt("Place2"))) 
        {
            PromScore2 = PlayerPrefs.GetInt("Place2"); //2 ����� ������������ � PromScore2
            PromScoreStr2 = PlayerPrefs.GetString("Place2Str");

            PlayerPrefs.SetInt("Place2", (int)playerScript.xp); //�������� playerScript.xp ���������� �� 2 �����'
            PlayerPrefs.SetString("Place2Str", Name.ggName);

            PromScore = PlayerPrefs.GetInt("Place3"); //�������� 3 ����� ������������ � PromScore
            PromScoreStr = PlayerPrefs.GetString("Place3Str");

            PlayerPrefs.SetInt("Place3", PromScore2); //PromScore 2 ������������ � 3 �����
            PlayerPrefs.SetString("Place3Str", PromScoreStr2);

            PromScore2 = PlayerPrefs.GetInt("Place4"); //�������� 4 ����� ������������ � Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", PromScore); //PromScore ������������ �� 4 �����
            PlayerPrefs.SetString("Place4Str", PromScoreStr);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 ������������ �� 5 �����
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }
        else if ((playerScript.xp > PlayerPrefs.GetInt("Place3")))
        {
            PromScore = PlayerPrefs.GetInt("Place3"); //�������� 3 ����� ������������ � PromScore
            PromScoreStr = PlayerPrefs.GetString("Place3Str");

            PlayerPrefs.SetInt("Place3", (int)playerScript.xp); //playerScript.xp ������������ � 3 �����
            PlayerPrefs.SetString("Place3Str", Name.ggName);

            PromScore2 = PlayerPrefs.GetInt("Place4"); //�������� 4 ����� ������������ � Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", PromScore); //PromScore ������������ �� 4 �����
            PlayerPrefs.SetString("Place4Str", PromScoreStr);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 ������������ �� 5 �����
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }


        else if ((playerScript.xp > PlayerPrefs.GetInt("Place4")))
        {
            PromScore2 = PlayerPrefs.GetInt("Place4"); //�������� playerScript.xp ����� ������������ � Promscore2
            PromScoreStr2 = PlayerPrefs.GetString("Place4Str");

            PlayerPrefs.SetInt("Place4", (int)playerScript.xp); //PromScore ������������ �� 4 �����
            PlayerPrefs.SetString("Place4Str", Name.ggName);

            PlayerPrefs.SetInt("Place5", PromScore2); // PromScore2 ������������ �� 5 �����
            PlayerPrefs.SetString("Place5Str", PromScoreStr2);
            PlayerPrefs.Save();
        }
        else if ((playerScript.xp > PlayerPrefs.GetInt("Place5")))
        {
            PlayerPrefs.SetInt("Place5", (int)playerScript.xp);
            PlayerPrefs.SetString("Place5Str", Name.ggName);
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
        bestScore.text = "������:" + PlayerPrefs.GetString("Place1Str") + ":"+ PlayerPrefs.GetInt("Place1").ToString();
        
    }
    public void onCLickExit()
    {
        Application.Quit();
    }
}
