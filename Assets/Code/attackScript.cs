using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class attackScript : MonoBehaviour
{
    public Button hill, attack, exit, shopHil, shopDam, shopArm, leaveBt;
    public static bool inShop = false, isLeave;
    public static int etFig = 1, damBon = 0, GameCount = 0, prevEn, randBon, randMoney, xpBon, moneyBon, damLast, hpLast, leaveCh, hpEnBon, xpK, Score, costDam, costHill, costArm;
    public Image enCur, hpBar, enhpBar, hpBarShab, enhpBarShab, xpBar;
    public Text dam, GGname, namEn, hpBarVal, EnhpBarVal, xpBarVal, hilkaVal, lvlVal,lvlValNext, damVal, moneyVal, ScoreText, costDamText, costArmText, costHillText;
    public Sprite im1, im2, im3, imCur, im4, im5, im6, im7, im8, im9, im10, shop, dead, butFig, butHil, butCon, clear;
    public AudioSource sound;
    public AudioClip damS, deadS, coinS;
    public static int enNumb = 1;
    public GameObject clickParent, damPrefab, damPrefabEn, clickParentEn, moneyParent, damParent, hilkaParent;
    public moveText[] clickTextPool = new moveText[10];
    private moveText[] clickTextPoolEn = new moveText[10];
    private moveText[] clickTextHilka = new moveText[10];
    private moveText[] clickTextMoney = new moveText[10];
    private moveText[] clickTextDam = new moveText[10];
    private void Start()
    {
        costArm = 20; costDam = 25; costHill = 10;
        Score = 0;
        xpK = 1;
        enemyScript.damag = 3;
        playerScript.hp = 20;
        playerScript.lvl = 1;
        playerScript.lvlNext = 10;
        playerScript.dam = (10 * playerScript.lvl);
        playerScript.damWea = playerScript.armor = playerScript.money = playerScript.damBonus = playerScript.hilka = attackScript.GameCount = 0;
        playerScript.xp = 0;
        playerScript.hpMax = (20 * playerScript.lvl);
        enemyScript.hpEnmax = 10;
        enemyScript.hpEn = enemyScript.hpEnmax;
        enemyScript.lvlEn = 1;
        enemyScript.damEn = (enemyScript.damag * enemyScript.lvlEn);
        inShop = false;
        leaveBt.gameObject.SetActive(false);
        shopHil.gameObject.SetActive(false);
        shopArm.gameObject.SetActive(false);
        shopDam.gameObject.SetActive(false);
        GGname.text = Name.ggName;
        if (inShop == false)
        {
            shopHil.gameObject.SetActive(false);
            shopArm.gameObject.SetActive(false);
            shopDam.gameObject.SetActive(false);
        }
        playerScript.lvl = 1;
        enCur.sprite = im1;
        namEn.text = "����� " + enemyScript.lvlEn.ToString() + " lvl";
        enemyScript.hpEn = enemyScript.hpEnmax;
        enNumb = 1;
        StartCoroutine(Defense());
        dam.text = "�������� �������� �� �*��!";
        if (playerScript.hilka > 0) //�������� �����
        {
            hill.gameObject.SetActive(true);
        }
        else
        {
            hill.gameObject.SetActive(false);
        }
        for (int i = 0; i<clickTextPool.Length; i++)
        {
            clickTextPool[i] = Instantiate(damPrefab, clickParent.transform).GetComponent<moveText>();
        }
        for (int j = 0; j < clickTextPool.Length; j++)
        {
            clickTextPoolEn[j] = Instantiate(damPrefabEn, clickParentEn.transform).GetComponent<moveText>();
        }
        for (int j = 0; j < clickTextPool.Length; j++)
        {
            clickTextHilka[j] = Instantiate(damPrefab, hilkaParent.transform).GetComponent<moveText>();
        }
        for (int j = 0; j < clickTextPool.Length; j++)
        {
            clickTextMoney[j] = Instantiate(damPrefab, moneyParent.transform).GetComponent<moveText>();
        }

        for (int j = 0; j < clickTextPool.Length; j++)
        {
            clickTextDam[j] = Instantiate(damPrefab, damParent.transform).GetComponent<moveText>();
        }

    }
    private void Update() //���������� ������ ����������
    {
        
        ScoreText.text = Score.ToString();
        xpBar.fillAmount = playerScript.xp / playerScript.lvlNext;
        xpBarVal.text = playerScript.xp.ToString() + "/" + playerScript.lvlNext.ToString() + " �����";
        playerScript.hpMax = (20 * playerScript.lvl) + playerScript.armor;
        playerScript.dam = (10 * playerScript.lvl) + playerScript.damBonus + playerScript.damWea;
        enhpBar.fillAmount = enemyScript.hpEn / enemyScript.hpEnmax;
        hpBar.fillAmount = playerScript.hp / playerScript.hpMax;
        EnhpBarVal.text = enemyScript.hpEn.ToString() + "hp";
        hpBarVal.text = playerScript.hp.ToString() + " hp";
        moneyVal.text = playerScript.money.ToString();
        hilkaVal.text = playerScript.hilka.ToString();
        lvlVal.text = playerScript.lvl.ToString();
        lvlValNext.text = (playerScript.lvl + 1).ToString();
        damVal.text = (playerScript.dam - playerScript.damBonus).ToString();
        costHill = playerScript.lvl * 10;
        costDam =  playerScript.lvl * 25;
        costArm =  playerScript.lvl * 20;
        costHillText.text = costHill.ToString();
        costDamText.text = costDam.ToString();
        costArmText.text = costArm.ToString();

        if (inShop == false)
        {
            shopHil.gameObject.SetActive(false);
            shopArm.gameObject.SetActive(false);
            shopDam.gameObject.SetActive(false);
        }
        if (inShop == true)
        {
            if (playerScript.money > costHill) shopHil.gameObject.SetActive(true);
            else shopHil.gameObject.SetActive(false);
            if (playerScript.money > costDam) shopDam.gameObject.SetActive(true);
            else shopDam.gameObject.SetActive(false);
            if (playerScript.money > costArm) shopArm.gameObject.SetActive(true);
            else shopArm.gameObject.SetActive(false);
        }
    }
    public void clickExitGame()
    {
        Application.Quit();
    }
   
   
    public void onClick() {
        damBon = UnityEngine.Random.Range(-5, 5);
        
        switch (etFig) //���� �����
        {
            case 0: //��������� �����
                StartCoroutine(Defense());
                break;
            case 1: //�����
                StartCoroutine(Attack());
                break;
            case 2: //����� ���������� ������� (�����)
                    inShop = false;
                    StartCoroutine(RandomEnent());
                    prevEn = enNumb;

                if (enemyScript.item == false)
                {
                    leaveBt.gameObject.SetActive(true);
                    enhpBarShab.gameObject.SetActive(true);
                    dam.text = "����������� � ����� �� ��������� �����������! ";
                    attack.GetComponentInChildren<Image>().sprite = butCon;
                    if (playerScript.hilka > 0) //�������� �����
                    {
                        hill.gameObject.SetActive(true);
                    }
                    else
                    {
                        hill.gameObject.SetActive(false);
                    }      
                }
                else if (enemyScript.item == true)
                {
                    hill.gameObject.SetActive(false);
                    randBon = UnityEngine.Random.Range(1, 10);
                }
                switch (enNumb)
                {
                    case 3:
                        hill.gameObject.SetActive(false);
                        playerScript.damWea += randBon;
                        dam.text = "�� ����� ����� ������! (+" + randBon.ToString() + " �����)";
                        playerScript.dam = (10 * playerScript.lvl) + playerScript.damBonus + playerScript.damWea;
                        clickTextDam[0].StartMotionXp(randBon);
                        etFig = 2;
                        break;
                    case 4:
                        hill.gameObject.SetActive(false);
                        playerScript.armor += randBon;
                        playerScript.hp += randBon;
                        dam.text = "�� ����� ������� ����! (+" + randBon.ToString() + " ������)";
                        clickTextPool[0].StartMotionHp(randBon);
                        etFig = 2;
                        break;
                    case 5:
                        hill.gameObject.SetActive(false);
                        playerScript.hilka += 1;
                        dam.text = "�� ����� ������!";
                        clickTextHilka[0].StartMotionXp(1); 
                        etFig = 2;
                        break;
                    case 7:
                        hill.gameObject.SetActive(false);
                        dam.text = "�� �������� ��������! ��� ����������� ����.";
                        inShop = true;
                        etFig = 2;
                        break;
                    case 10:
                        hill.gameObject.SetActive(false);
                        playerScript.hp = playerScript.hpMax;
                        dam.text = "�� ��������� ����� ����! ��� �������� ���������� �� ���������.";
                        etFig = 2;
                        break;
                    default:
                        attack.GetComponentInChildren<Image>().sprite = butFig; 
                        enNumb = 2;
                        etFig = 1;
                        break;
                }
                break;
            default:
                break;
        }
    }
    // �������
   
    public void BuyClickHill() //������� �����
    {
        if (playerScript.money >= costHill)
        {
            playerScript.money -= costHill;
            playerScript.hilka++;
            sound.PlayOneShot(coinS);
            dam.text = "�� ������� ������ �����!";
            clickTextHilka[0].StartMotionXp(1);
        }
    }
    public void BuyClickDam() //������� �����
    {
        if (playerScript.money >= costDam)
        {
            sound.PlayOneShot(coinS);
            playerScript.money -= costDam;
            playerScript.damWea += 10;
            dam.text = "�� ������� ������ ������! ���� �������� �� 10.";
            clickTextDam[0].StartMotionXp(10);
        }
    }
    public void BuyClickArmor() //������� �����
    {
        if (playerScript.money >= costArm)
        {
            sound.PlayOneShot(coinS);
            playerScript.money -= costArm;
            playerScript.armor += 10;
            playerScript.hp += 10;
            clickTextPool[0].StartMotionHp(10);
            dam.text = "�� ������� ������ ������� ����! ������ ��������� �� 10.";
        }
    }
    public void leaveClick() //�����
    {
        leaveCh = UnityEngine.Random.Range(1, 30);
        if (leaveCh - enemyScript.lvlEn > 0)
        {
            isLeave = true;
            enNumb = prevEn;
            StartCoroutine(enemyDead());
            etFig = 2;
        }
        else
        {
            dam.text = "����� �� ������!";
            StartCoroutine(Defense());
        }
    }

    public void onClickRegen() //����
    {
        attackScript.hpLast = (int)playerScript.hp;
        playerScript.hp = playerScript.hpMax;
        playerScript.hilka -= 1;
        GetComponent<attackScript>().clickTextPool[0].StartMotionHp(playerScript.hpMax - attackScript.hpLast);
        GetComponent<attackScript>().dam.text = "�� ������������ ��� ��������";
        if (enemyScript.hpEn >= 0)
        StartCoroutine(GetComponent<attackScript>().Defense());
 
    }
    public IEnumerator Attack() //����� 
    {
        enhpBarShab.gameObject.SetActive(true);
        leaveBt.gameObject.SetActive(false);
        enemyScript.hpEn -= (playerScript.dam + damBon);
        dam.text = "�� ������� " + (playerScript.dam + damBon).ToString() + " �����!";
        clickTextPoolEn[0].StartMotion(playerScript.dam + damBon);
        sound.PlayOneShot(damS);
        if (enemyScript.hpEn <= 0)
        {
            isLeave = false;
            StartCoroutine(enemyDead());
            etFig = 2;
        }
        else
        {
            attack.gameObject.SetActive(false);
            hill.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            StartCoroutine(Defense());
        }
    }
    public IEnumerator enemyDead() //������� ������ �����
    {
        if (isLeave == false) //����� ���� ����
        { 
            enCur.GetComponent<Animation>().Play("dead");
            namEn.text = "";
            enemyScript.hpEn = 0;
            hill.gameObject.SetActive(false);
            attack.gameObject.SetActive(false);
            leaveBt.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            attack.gameObject.SetActive(true);
            randMoney = UnityEngine.Random.Range((-4 * xpK), (5 * xpK));
            moneyBon = (5 * xpK) + randMoney + enemyScript.lvlEn;
            enhpBarShab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0001f);
            xpBon = xpK * 5 + enemyScript.lvlEn * 2;
            Score += (xpK * 5 + enemyScript.lvlEn * 2);
            attack.GetComponentInChildren<Image>().sprite = butCon;
            playerScript.xp += xpBon;
            playerScript.money += moneyBon;
            clickTextMoney[0].StartMotionXp(moneyBon);
            enCur.color = new Color(255, 255, 255, 255);
            dam.text = $"��������� ���������! �� �������� {xpBon} ����� � {moneyBon} �����.";
            enCur.sprite = dead;
                if (playerScript.xp >= playerScript.lvlNext)
                {
                
                damLast = (playerScript.dam);
                hpLast = (int)playerScript.hp;
                sound.PlayOneShot(coinS);
                playerScript.lvl++;
                playerScript.xp -= playerScript.lvlNext;
                playerScript.lvlNext = playerScript.lvl * (25 * playerScript.lvl);
                playerScript.hpMax = (playerScript.lvl * 20) + playerScript.armor;
                playerScript.hp = playerScript.hpMax;
                playerScript.dam = (10 * playerScript.lvl) + playerScript.damBonus;
                                   
                    clickTextPool[0].StartMotionHp(playerScript.hpMax - hpLast);
                    clickTextDam[0].StartMotionXp(playerScript.dam - damLast);
                }
                else
                {
                    sound.PlayOneShot(deadS);
                }
        }
        else //����� �� ������
        {
            enCur.GetComponent<Animation>().Play("dead");
            namEn.text = "";
            enemyScript.hpEn = 0;
            hill.gameObject.SetActive(false);
            attack.gameObject.SetActive(false);
            leaveBt.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            attack.gameObject.SetActive(true);
            enhpBarShab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0001f);
            attack.GetComponentInChildren<Image>().sprite = butCon;
            enCur.color = new Color(255, 255, 255, 255);
            dam.text = "�� ������� �������!";
            enCur.sprite = clear;

        }
    }
    public IEnumerator Defense() //�������� �����
    {
        inShop = false;
        attack.gameObject.SetActive(false);
        hill.gameObject.SetActive(false);
        leaveBt.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        sound.PlayOneShot(damS);
        enemyScript.damEn = (enemyScript.damag * enemyScript.lvlEn);
        damBon = UnityEngine.Random.Range(-5, 5);
        playerScript.hp -= (enemyScript.damEn + damBon);
        clickTextPool[0].StartMotion(enemyScript.damEn + damBon); 
        dam.text = "���� ����� " + (enemyScript.damEn + damBon).ToString() + " �����!";
        attack.GetComponentInChildren<Image>().sprite = butFig;
        etFig = 1;
        yield return new WaitForSeconds(1f);
        attack.gameObject.SetActive(true);
        leaveBt.gameObject.SetActive(true);
        if (playerScript.hilka > 0) //�������� �����
        {
            hill.gameObject.SetActive(true);
        }
        else
        {
            hill.gameObject.SetActive(false);
        }
    }

    public IEnumerator RandomEnent() //����� ���������� ���������� (�������)
    {
        shopHil.gameObject.SetActive(false);
        shopArm.gameObject.SetActive(false);
        shopDam.gameObject.SetActive(false);
        enCur.color = new Color(255, 255, 255, 255);
        while (enNumb == prevEn) 
        {
            if (playerScript.lvl < 2)
            {
                enNumb = UnityEngine.Random.Range(1, 4);
            }
            else if (playerScript.lvl >= 2 && playerScript.lvl < 4)
            {
                enNumb = UnityEngine.Random.Range(1, 8);
            }
            else if (playerScript.lvl >= 4 && playerScript.lvl < 6)
            {
                enNumb = UnityEngine.Random.Range(1, 11);
            }
            else if (playerScript.lvl >= 6 && playerScript.lvl < 7)
            {
                enNumb = UnityEngine.Random.Range(1, 13);
            }
            else if (playerScript.lvl >= 7 && playerScript.lvl < 8)
            {
                enNumb = UnityEngine.Random.Range(1, 14);
            }
            else if (playerScript.lvl >= 8 && playerScript.lvl < 10)
            {
                enNumb = UnityEngine.Random.Range(1, 15);
            }
            else if (playerScript.lvl >= 10)
            {
                enNumb = UnityEngine.Random.Range(1, 16);
            }
            if (playerScript.money <= costHill && enNumb == 7)
            {
                enNumb = UnityEngine.Random.Range(1, 7);
            }
            if (playerScript.hp == playerScript.hpMax && enNumb == 10)
            {
                enNumb = UnityEngine.Random.Range(1, 10);
            }
        }
        switch (enNumb)
        {
            case 1:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl, playerScript.lvl+2);
                xpK = 1;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 3;
                enemyScript.hpEnmax = 10 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "����� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im1;
                enemyScript.item = false;
                

                break;
            case 2:
                if (playerScript.lvl >= 2)
                {
                    enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl, playerScript.lvl + 2);
                }
                else
                {
                    enemyScript.lvlEn = 1;
                }
                xpK = 2;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 4;
                enemyScript.hpEnmax = 15 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "��� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im2;
                enemyScript.item = false;
                break;

            case 3:
                enemyScript.lvlEn = 0;
                namEn.text = "������";
                enCur.sprite = im3;
                enemyScript.item = true;
                break;
            case 4:
                enemyScript.lvlEn = 0;
                namEn.text = "������� ����";
                enCur.sprite = im4;
                enemyScript.item = true;
                break;
            case 5:
                enemyScript.lvlEn = 0;
                namEn.text = "������";
                enCur.sprite = im5;
                enemyScript.item = true;
                break;
            case 6:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl - 1, playerScript.lvl + 3);
                xpK = 3;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 5;
                enemyScript.hpEnmax = 30 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "���� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im6;
                enemyScript.item = false;
                break;
            case 7:
                enemyScript.hpEnmax = 75;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 5;
                namEn.text = "��������";
                enCur.sprite = im8;
                enemyScript.item = true;
                inShop = true;
                break;
            case 8:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl, playerScript.lvl + 3);
                xpK = 4;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 6;
                enemyScript.hpEnmax = 40 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "���� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im7;
                enemyScript.item = false;
                break;
            case 9:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl - 1, playerScript.lvl + 4);
                xpK = 5;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 6;
                enemyScript.hpEnmax = 50 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "������ (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im8;
                enemyScript.item = false;
                break;
            case 10:
                enemyScript.lvlEn = 0;
                namEn.text = "����� ����";
                enCur.sprite = im5;
                enemyScript.item = true;
                break;
            case 11:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl+2, playerScript.lvl + 4);
                xpK = 6;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 7;
                enemyScript.hpEnmax = 60 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "����� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 12:
                if (playerScript.lvl >= 2)
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl + 2, playerScript.lvl + 7);
                xpK = 7;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 6;
                enemyScript.hpEnmax = 70 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "���� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 13:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl+3, playerScript.lvl + 8);
                xpK = 8;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 6;
                enemyScript.hpEnmax = 100 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "���� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 14:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl+4, playerScript.lvl + 8);
                xpK = 9;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 8;
                enemyScript.hpEnmax = 125 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax;
                namEn.text = "���� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 15:
                enemyScript.lvlEn = UnityEngine.Random.Range(playerScript.lvl+5, playerScript.lvl + 12);
                xpK = 10;
                hpEnBon = 5 * enemyScript.lvlEn;
                enemyScript.damag = 200;
                enemyScript.hpEnmax = 200 * enemyScript.lvlEn;
                enemyScript.hpEn = enemyScript.hpEnmax; 
                namEn.text = "��� (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            default:
                enemyScript.hpEn = 1000;
                namEn.text = "��������� ��� (" + enemyScript.lvlEn.ToString() + " lvl)";
                break;
        }
        yield return new WaitForSeconds(1f);
    }
}
