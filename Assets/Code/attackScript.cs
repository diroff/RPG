using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.PostProcessing;
public class attackScript : MonoBehaviour
{
    public PostProcessVolume chB;
    public Button hill, attack, continueBt, exit, shopHil, shopDam, shopArm, leaveBt, fightMiniGameBt, pauseResumeBt, pauseStopBt;
    public static bool inShop = false, isLeave, isClickFight, paused;
    public static int etFig = 1, damBon = 0, GameCount = 0, prevEn, randBon, randMoney, xpBon, moneyBon, damLast, hpLast, leaveCh, hpEnBon, xpK, Score, costDam, costHill, costArm, leaveCount;
    public Image enCur, hpBar, enhpBar, hpBarShab, enhpBarShab, xpBar, powerDam, powerDamSlider, backGround;
    public Text dam, GGname, namEn, hpBarVal, EnhpBarVal, xpBarVal, hilkaVal, lvlVal,lvlValNext, damVal, moneyVal, ScoreText, costDamText, costArmText, costHillText;
    public Sprite im1, im2, im3, imCur, im4, im5, im6, im7, im8, im9, im10, shop, dead, butFig, butHil, clear;
    public Vector2 direction;
    public AudioSource sound;
    public AudioClip damS, deadS, coinS;
    public static int enNumb = 1;
    public float startDistance, damBonMinigame, speedMove, hpEnfl, hpFl;
    public GameObject clickParent, damPrefab, damPrefabEn, clickParentEn, moneyParent, damParent, hilkaParent;
    public moveText[] clickTextPool = new moveText[10];
    private moveText[] clickTextPoolEn = new moveText[10];
    private moveText[] clickTextHilka = new moveText[10];
    private moveText[] clickTextMoney = new moveText[10];
    private moveText[] clickTextDam = new moveText[10];
    private void Start()
    {
        continueBt.gameObject.SetActive(false);
        chB.enabled = true;
        paused = true;
        pauseResumeBt.gameObject.SetActive(false);
        pauseStopBt.gameObject.SetActive(true);
        fightMiniGameBt.gameObject.SetActive(false);
        powerDam.gameObject.SetActive(false);
        powerDamSlider.gameObject.SetActive(false);
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
        leaveCount = 25 + playerScript.lvl;
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
        namEn.text = "Комар " + enemyScript.lvlEn.ToString() + " lvl";
        enemyScript.hpEn = enemyScript.hpEnmax;
        enNumb = 1;
        StartCoroutine(Defense());
        dam.text = "Готовься получить по ж*пе!";
        if (playerScript.hilka > 0) //проверка хилок
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
    private void Update() //обновление нужных переменных
    {
        hpFl = playerScript.hp;
        hpEnfl = enemyScript.hpEn;
        ScoreText.text = Score.ToString();
        xpBar.fillAmount = playerScript.xp / playerScript.lvlNext;
        xpBarVal.text = playerScript.xp.ToString() + "/" + playerScript.lvlNext.ToString() + " опыта";
        playerScript.hpMax = (20 * playerScript.lvl) + playerScript.armor;
        playerScript.dam = (10 * playerScript.lvl) + playerScript.damBonus + playerScript.damWea;
        enhpBar.fillAmount = hpEnfl / enemyScript.hpEnmax;
        hpBar.fillAmount = hpFl / playerScript.hpMax;
        EnhpBarVal.text = enemyScript.hpEn.ToString();
        hpBarVal.text = playerScript.hp.ToString();
        moneyVal.text = playerScript.money.ToString();
        hilkaVal.text = playerScript.hilka.ToString();
        lvlVal.text = playerScript.lvl.ToString();
        lvlValNext.text = (playerScript.lvl + 1).ToString();
        damVal.text = (playerScript.dam - playerScript.damBonus).ToString();
        costHill = (playerScript.lvl-1) * 15;
        costDam = (playerScript.lvl - 1) * 25;
        costArm = (playerScript.lvl - 1) * 20;
        costHillText.text = costHill.ToString();
        costDamText.text = costDam.ToString();
        costArmText.text = costArm.ToString();
        //пауза
        if (paused)
        {
            Time.timeScale = 0;
            pauseResumeBt.gameObject.SetActive(true);
            pauseStopBt.gameObject.SetActive(false);
            chB.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseStopBt.gameObject.SetActive(true);
            pauseResumeBt.gameObject.SetActive(false);
            chB.enabled = false;
        }
        //проверка магаза
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
    public void clickExitGame() //выход из игры
    {
        Application.Quit();
    }
    public void OnClickPause() //нажатие на паузу
    {
        paused = !paused;
    }

    public void onClick() { //ход игры
        damBon = UnityEngine.Random.Range(-5, 5);
        
        switch (etFig) //шаги битвы
        {
            case 0: //получение урона
                StartCoroutine(Defense());
                break;
            case 1: //атака
                StartCoroutine(Attack());
                break;
            case 2: //выбор следующего события (врага)
                
                break;
            default:
                break;
        }
    }
    // События
   
    public void BuyClickHill() //покупка хилки
    {
        if (playerScript.money >= costHill)
        {
            playerScript.money -= costHill;
            playerScript.hilka++;
            sound.PlayOneShot(coinS);
            dam.text = "Вы успешно купили хилку!";
            clickTextHilka[0].StartMotionXp(1);
        }
    }
    public void BuyClickDam() //покупка урона
    {
        if (playerScript.money >= costDam)
        {
            sound.PlayOneShot(coinS);
            playerScript.money -= costDam;
            playerScript.damWea += 10;
            dam.text = "Вы успешно купили кинжал! Урон увеличен на 10.";
            clickTextDam[0].StartMotionXp(10);
        }
    }
    public void BuyClickArmor() //покупка брони
    {
        if (playerScript.money >= costArm)
        {
            sound.PlayOneShot(coinS);
            playerScript.money -= costArm;
            playerScript.armor += 10;
            playerScript.hp += 10;
            clickTextPool[0].StartMotionHp(10);
            dam.text = "Вы успешно купили кусочек кожи! Защита увеличена на 10.";
        }
    }
    public void leaveClick() //побег
    {
        leaveCh = UnityEngine.Random.Range(1, leaveCount);
        if (leaveCh - enemyScript.lvlEn - xpK > 0)
        {
            isLeave = true;
            enNumb = prevEn;
            StartCoroutine(enemyDead());
            etFig = 2;
            leaveCount--;
        }
        else
        {
            dam.text = "Побег не удался!";
            StartCoroutine(Defense());
        }
    }

    public void onClickRegen() //хилл
    {
        attackScript.hpLast = (int)playerScript.hp;
        playerScript.hp = playerScript.hpMax;
        playerScript.hilka -= 1;
        clickTextPool[0].StartMotionHp(playerScript.hpMax - attackScript.hpLast);
        dam.text = "Вы восстановили своё здоровье";
        if (enemyScript.hpEn >= 0)
        StartCoroutine(Defense());
 
    }
    public void AttackMiniGameClick()
    {
        isClickFight = true;
    }
    public void OnClickContinue()
    {
        StartCoroutine(ContinCor());
    }
    public IEnumerator ContinCor()
    {
        continueBt.gameObject.SetActive(false);
        backGround.GetComponent<Animation>().Play("newEnemy");
        namEn.text = "";
        yield return new WaitForSeconds(2.0f);
        attack.gameObject.SetActive(false);
        inShop = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RandomEnent());
        prevEn = enNumb;
        
        backGround.gameObject.transform.localPosition = new Vector2(960, 0);
        if (enemyScript.item == false)
        {
            backGround.gameObject.transform.localPosition = new Vector2(960, 0);
            leaveBt.gameObject.SetActive(true);
            enhpBarShab.gameObject.SetActive(true);
            attack.gameObject.SetActive(true);

            //тут что-то надо
            dam.text = "Приготовься к битве со следующим противником! ";
            if (playerScript.hilka > 0) //проверка хилок
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
            backGround.gameObject.transform.localPosition = new Vector2(960, 0);
            hill.gameObject.SetActive(false);
            attack.gameObject.SetActive(false);
            continueBt.gameObject.SetActive(true);
            randBon = UnityEngine.Random.Range(1, 10);
        }
        enCur.GetComponent<Animation>().Play("deadRev");
        switch (enNumb)
        {
            case 3:
                hill.gameObject.SetActive(false);
                playerScript.damWea += randBon;
                dam.text = "Ты нашёл новый кинжал! (+" + randBon.ToString() + " урона)";
                playerScript.dam = (10 * playerScript.lvl) + playerScript.damBonus + playerScript.damWea;
                clickTextDam[0].StartMotionXp(randBon);
                etFig = 2;
                break;
            case 4:
                hill.gameObject.SetActive(false);
                playerScript.armor += randBon;
                playerScript.hp += randBon;
                dam.text = "Ты нашёл кусочек кожи! (+" + randBon.ToString() + " защиты)";
                clickTextPool[0].StartMotionHp(randBon);
                etFig = 2;
                break;
            case 5:
                hill.gameObject.SetActive(false);
                playerScript.hilka += 1;
                dam.text = "Ты нашёл пузырёк!";
                clickTextHilka[0].StartMotionXp(1);
                etFig = 2;
                break;
            case 7:
                hill.gameObject.SetActive(false);
                dam.text = "Ты встретил торговца! Его предложения ниже.";
                inShop = true;
                etFig = 2;
                break;
            case 10:
                hill.gameObject.SetActive(false);
                playerScript.hp = playerScript.hpMax;
                dam.text = "Ты обнаружил место силы! Твоё здоровье восполнено до максимума.";
                etFig = 2;
                break;
            default:
                attack.GetComponentInChildren<Image>().sprite = butFig;
                enNumb = 2;
                etFig = 1;
                break;
        }

    }
    public IEnumerator Attack() //атака 
    {
        enhpBarShab.gameObject.SetActive(true);
        leaveBt.gameObject.SetActive(false);
        attack.gameObject.SetActive(false);
        hill.gameObject.SetActive(false);
        powerDam.gameObject.SetActive(true);
        powerDamSlider.gameObject.SetActive(true);
        speedMove = 300 + UnityEngine.Random.Range(0, 15) + (15*xpK);
        isClickFight = false;
        startDistance = -250;
        fightMiniGameBt.gameObject.SetActive(true);
        while (startDistance <= 250 && isClickFight == false)
        {
            yield return new WaitForSeconds(0.0000001f);
            startDistance += speedMove * Time.deltaTime;
            powerDamSlider.gameObject.transform.localPosition = new Vector2(startDistance, 0);
        }
        fightMiniGameBt.gameObject.SetActive(false);
        damBonMinigame = (int)startDistance;
        yield return new WaitForSeconds(0.5f);
        if ((startDistance >= -250 && startDistance < -150) || (startDistance >= 150)) damBonMinigame = UnityEngine.Random.Range(0, 0.3f);
        if ((startDistance >= -150 && startDistance < -80) || (startDistance >= 80 && startDistance < 150)) damBonMinigame = UnityEngine.Random.Range(0.3f, 0.5f);
        if ((startDistance >= -80 && startDistance < -20) ||  (startDistance >= 20 && startDistance < 80)) damBonMinigame = UnityEngine.Random.Range(0.5f, 0.99f);
        if (startDistance >= -20 && startDistance < 20) damBonMinigame = UnityEngine.Random.Range(1f, 2.5f);
        powerDam.gameObject.SetActive(false);
        powerDamSlider.gameObject.SetActive(false);
        if ((Int32)((playerScript.dam + damBon) * damBonMinigame) <= 0)
        {
            dam.text = "Промах!";
        }
        else
        {
            enemyScript.hpEn -= (Int32)((playerScript.dam + damBon) * damBonMinigame);
            dam.text = "Вы нанесли " + ((Int32)((playerScript.dam + damBon) * damBonMinigame)).ToString() + " урона! ";
            clickTextPoolEn[0].StartMotion((Int32)((playerScript.dam + damBon) * damBonMinigame));
            sound.PlayOneShot(damS);
        }
        
        if (enemyScript.hpEn <= 0)
        {
            isLeave = false;
            StartCoroutine(enemyDead());
            etFig = 2;
        }
        else
        {
            dam.text += "Готовься получить по ж*пе!";
            attack.gameObject.SetActive(false);
            hill.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            StartCoroutine(Defense());
        }
    }
 
    public IEnumerator enemyDead() //событие смерти врага
    {
        if (isLeave == false) //когда враг умер
        { 
            enCur.GetComponent<Animation>().Play("dead");
            namEn.text = "";
            enemyScript.hpEn = 0;
            hill.gameObject.SetActive(false);
            attack.gameObject.SetActive(false);
            leaveBt.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            randMoney = UnityEngine.Random.Range((-4 * xpK), (5 * xpK));
            moneyBon = (5 * xpK) + randMoney + enemyScript.lvlEn;
            enhpBarShab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0001f);
            xpBon = xpK * 5 + enemyScript.lvlEn * 2;
            Score += (xpK * 5 + enemyScript.lvlEn * 2);
            //тут что-то изменить
            playerScript.xp += xpBon;
            playerScript.money += moneyBon;
            clickTextMoney[0].StartMotionXp(moneyBon);
            enCur.color = new Color(255, 255, 255, 255);
            dam.text = $"Противник уничтожен! Вы получили {xpBon} опыта и {moneyBon} монет.";
            enCur.sprite = dead;
                if (playerScript.xp >= playerScript.lvlNext)
                {
                damLast = (playerScript.dam);
                hpLast = (int)playerScript.hp;
                sound.PlayOneShot(coinS);
                playerScript.lvl++;
                leaveCount = 25 + playerScript.lvl;
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
        else //когда гг сбежал
        {
            enCur.GetComponent<Animation>().Play("dead");
            namEn.text = "";
            enemyScript.hpEn = 0;
            hill.gameObject.SetActive(false);
            attack.gameObject.SetActive(false);
            leaveBt.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            enhpBarShab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0001f);
            //тут что-то изменить
            enCur.color = new Color(255, 255, 255, 255);
            dam.text = "Вы успешно убежали!";
            enCur.sprite = clear;
        }
        attack.gameObject.SetActive(false);
        continueBt.gameObject.SetActive(true);
    }
    public IEnumerator Defense() //принятие удара
    {
        inShop = false;
        attack.gameObject.SetActive(false);
        hill.gameObject.SetActive(false);
        leaveBt.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        //хренозащита
        powerDam.gameObject.SetActive(true);
        powerDamSlider.gameObject.SetActive(true);
        speedMove = 350 + UnityEngine.Random.Range(0, 15) + (15 * xpK);
        isClickFight = false;
        startDistance = -250;
        fightMiniGameBt.gameObject.SetActive(true);
        while (startDistance <= 250 && isClickFight == false)
        {
            yield return new WaitForSeconds(0.0000001f);
            startDistance += speedMove * Time.deltaTime;
            powerDamSlider.gameObject.transform.localPosition = new Vector2(startDistance, 0);
        }
        fightMiniGameBt.gameObject.SetActive(false);
        damBonMinigame = (int)startDistance;
        yield return new WaitForSeconds(0.5f);
        if ((startDistance >= -250 && startDistance < -150) || (startDistance >= 150)) damBonMinigame = UnityEngine.Random.Range(1.5f, 2f);
        if ((startDistance >= -150 && startDistance < -80) || (startDistance >= 80 && startDistance < 150)) damBonMinigame = UnityEngine.Random.Range(0.9f, 1.4f);
        if ((startDistance >= -80 && startDistance < -20) || (startDistance >= 20 && startDistance < 80)) damBonMinigame = UnityEngine.Random.Range(0.8f, 0.9f);
        if (startDistance >= -20 && startDistance < 20) damBonMinigame = UnityEngine.Random.Range(0.3f, 0.7f);
        powerDam.gameObject.SetActive(false);
        powerDamSlider.gameObject.SetActive(false);
        damBon = UnityEngine.Random.Range(-5, 5);
        enemyScript.damEn = (Int32)(((enemyScript.damag * enemyScript.lvlEn) + damBon)*damBonMinigame);
        if (enemyScript.damEn <= 0)
        {
            enemyScript.damEn = 0;
            dam.text = "Промах!";
        }
        else
        {
        playerScript.hp -= enemyScript.damEn;
        clickTextPool[0].StartMotion(enemyScript.damEn); 
        dam.text = "Враг нанес " + (enemyScript.damEn).ToString() + " урона!";
        sound.PlayOneShot(damS);
        }
        attack.GetComponentInChildren<Image>().sprite = butFig;
        etFig = 1;
        yield return new WaitForSeconds(1f);
        attack.gameObject.SetActive(true);
        leaveBt.gameObject.SetActive(true);
        if (playerScript.hilka > 0) //проверка хилок
        {
            hill.gameObject.SetActive(true);
        }
        else
        {
            hill.gameObject.SetActive(false);
        }
    }
    public IEnumerator RandomEnent() //выбор рандомного противника (события)
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
                namEn.text = "Комар (" + enemyScript.lvlEn.ToString() + " lvl)";
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
                namEn.text = "Жук (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im2;
                enemyScript.item = false;
                break;

            case 3:
                enemyScript.lvlEn = 0;
                namEn.text = "Кинжал";
                enCur.sprite = im3;
                enemyScript.item = true;
                break;
            case 4:
                enemyScript.lvlEn = 0;
                namEn.text = "Полоска кожи";
                enCur.sprite = im4;
                enemyScript.item = true;
                break;
            case 5:
                enemyScript.lvlEn = 0;
                namEn.text = "Пузырёк";
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
                namEn.text = "Жаба (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im6;
                enemyScript.item = false;
                break;
            case 7:
                enemyScript.hpEnmax = 75;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 5;
                namEn.text = "Торговец";
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
                namEn.text = "Крот (" + enemyScript.lvlEn.ToString() + " lvl)";
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
                namEn.text = "Голубь (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im8;
                enemyScript.item = false;
                break;
            case 10:
                enemyScript.lvlEn = 0;
                namEn.text = "Место силы";
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
                namEn.text = "Сокол (" + enemyScript.lvlEn.ToString() + " lvl)";
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
                namEn.text = "Волк (" + enemyScript.lvlEn.ToString() + " lvl)";
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
                namEn.text = "Рысь (" + enemyScript.lvlEn.ToString() + " lvl)";
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
                namEn.text = "Тигр (" + enemyScript.lvlEn.ToString() + " lvl)";
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
                namEn.text = "Лев (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            default:
                enemyScript.hpEn = 1000;
                namEn.text = "Секретный чел (" + enemyScript.lvlEn.ToString() + " lvl)";
                break;
        }
        yield return new WaitForSeconds(1f);
    }
}
