using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class attackScript : MonoBehaviour
{
    public Button hill, attack, exit, shopHil, shopDam, shopArm, leaveBt;
    public static bool inShop = false, isLeave;
    public static int etFig = 1, damBon = 0, GameCount = 0, prevEn, randBon, randMoney, xpBon, moneyBon, damLast, hpLast, leaveCh;
    public Image enCur, hpBar, enhpBar, hpBarShab, enhpBarShab, xpBar;
    public Text dam, GGname, namEn, hpBarVal, EnhpBarVal, xpBarVal, hilkaVal, lvlVal,lvlValNext, damVal, moneyVal;
    public Sprite im1, im2, im3, imCur, im4, im5, im6, im7, im8, im9, im10, shop, dead, butFig, butHil, butCon, clear;
    public AudioSource sound;
    public AudioClip damS, deadS, coinS;
    public static int enNumb = 1;
    public GameObject clickParent, damPrefab, damPrefabEn, clickParentEn, moneyParent, damParent, hilkaParent;
    public moveText[] clickTextPool = new moveText[1];
    private moveText[] clickTextPoolEn = new moveText[1];
    private moveText[] clickTextHilka = new moveText[10];
    private moveText[] clickTextMoney = new moveText[10];
    private moveText[] clickTextDam = new moveText[10];
    private void Start()
    {
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
        enemyScript.damEn = enemyScript.lvlEn * 5;
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

        xpBar.fillAmount = playerScript.xp / playerScript.lvlNext;
        xpBarVal.text = playerScript.xp.ToString() + "/" + playerScript.lvlNext.ToString() + " опыта";
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
    }
    public void clickExitGame()
    {
        Application.Quit();
    }
   
   
    public void onClick() {
        damBon = Random.Range(-5, 5);
        
        switch (etFig) //шаги битвы
        {
            case 0: //получение урона
                StartCoroutine(Defense());
                break;
            case 1: //атака
                StartCoroutine(Attack());
                break;
            case 2: //выбор следующего события (врага)

                    StartCoroutine(RandomEnent());
                    prevEn = enNumb;

                if (enemyScript.item == false)
                {
                    leaveBt.gameObject.SetActive(true);
                    enhpBarShab.gameObject.SetActive(true);
                    dam.text = "Приготовься к битве со следующим противником! ";
                    attack.GetComponentInChildren<Image>().sprite = butCon;
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
                    hill.gameObject.SetActive(false);
                    randBon = Random.Range(1, 10);
                }
                switch (enNumb)
                {
                    case 3:
                        hill.gameObject.SetActive(false);
                        playerScript.damWea += randBon;
                        dam.text = "Ты нашёл новый кинжал! (+" + randBon.ToString() + " урона)";
                        playerScript.dam = (10 * playerScript.lvl) + playerScript.damBonus + playerScript.damWea;
                        clickTextDam[0].StartMotionXp(randBon);
                        GameCount++;
                        etFig = 2;
                        break;
                    case 4:
                        hill.gameObject.SetActive(false);
                        playerScript.armor += randBon;
                        playerScript.hp += randBon;
                        dam.text = "Ты нашёл кусочек кожи! (+" + randBon.ToString() + " защиты)";
                        clickTextPool[0].StartMotionHp(randBon);
                        GameCount++;
                        etFig = 2;
                        break;
                    case 5:
                        hill.gameObject.SetActive(false);
                        playerScript.hilka += 1;
                        dam.text = "Ты нашёл пузырёк!";
                        clickTextHilka[0].StartMotionXp(1); 
                        GameCount++;
                        etFig = 2;
                        break;
                    case 7:
                        hill.gameObject.SetActive(false);
                        dam.text = "Ты встретил торговца! Его предложения ниже.";
                        if (playerScript.money >= 20)
                        {
                            shopHil.gameObject.SetActive(true);
                            shopArm.gameObject.SetActive(true);
                            shopDam.gameObject.SetActive(true);
                        }
                        else if(playerScript.money >= 10 && playerScript.money < 20)
                        {
                            shopHil.gameObject.SetActive(true);
                        }
                        else
                        {
                            shopHil.gameObject.SetActive(false);
                            shopArm.gameObject.SetActive(false);
                            shopDam.gameObject.SetActive(false);
                        }
                        GameCount++;
                        etFig = 2;
                        break;
                    case 10:
                        hill.gameObject.SetActive(false);
                        playerScript.hp = playerScript.hpMax;
                        dam.text = "Ты обнаружил место силы! Твоё здоровье восполнено до максимума.";
                        GameCount++;
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
    // События
   
    public void BuyClickHill() //покупка хилки
    {
        if (playerScript.money >= 10)
        {
            playerScript.money -= 10;
            playerScript.hilka++;
            sound.PlayOneShot(coinS);
            dam.text = "Вы успешно купили хилку!";
            clickTextHilka[0].StartMotionXp(1);
            if (playerScript.money >= 20)
            {
                shopHil.gameObject.SetActive(true);
                shopArm.gameObject.SetActive(true);
                shopDam.gameObject.SetActive(true);
            }
            else if (playerScript.money >= 10 && playerScript.money < 20)
            {
                shopHil.gameObject.SetActive(true);
                shopArm.gameObject.SetActive(false);
                shopDam.gameObject.SetActive(false);
            }
            else
            {
                shopHil.gameObject.SetActive(false);
                shopArm.gameObject.SetActive(false);
                shopDam.gameObject.SetActive(false);
            }
        }
    }
    public void SuicideClick() //суесыд
    {
        playerScript.hp = 0;
    }
    public void BuyClickDam() //покупка урона
    {
        if (playerScript.money >= 20)
        {
            sound.PlayOneShot(coinS);
            playerScript.money -= 20;
            playerScript.damWea += 10;
            dam.text = "Вы успешно купили кинжал! Урон увеличен на 10.";
            clickTextDam[0].StartMotionXp(10);
            if (playerScript.money >= 20)
            {
                shopHil.gameObject.SetActive(true);
                shopArm.gameObject.SetActive(true);
                shopDam.gameObject.SetActive(true);
            }
            else if (playerScript.money >= 10 && playerScript.money < 20)
            {
                shopHil.gameObject.SetActive(true);
                shopArm.gameObject.SetActive(false);
                shopDam.gameObject.SetActive(false);
            }
            else
            {
                shopHil.gameObject.SetActive(false);
                shopArm.gameObject.SetActive(false);
                shopDam.gameObject.SetActive(false);
            }
        }
    }
    public void BuyClickArmor() //покупка брони
    {
        if (playerScript.money >= 20)
        {
            sound.PlayOneShot(coinS);
            playerScript.money -= 20;
            playerScript.armor += 10;
            playerScript.hp += 10;
            clickTextPool[0].StartMotionHp(10);
            dam.text = "Вы успешно купили кусочек кожи! Защита увеличена на 10.";
            if (playerScript.money >= 20)
            {
                shopHil.gameObject.SetActive(true);
                shopArm.gameObject.SetActive(true);
                shopDam.gameObject.SetActive(true);
            }
            else if (playerScript.money >= 10 && playerScript.money < 20)
            {
                shopHil.gameObject.SetActive(true);
                shopArm.gameObject.SetActive(false);
                shopDam.gameObject.SetActive(false);
            }
            else
            {
                shopHil.gameObject.SetActive(false);
                shopArm.gameObject.SetActive(false);
                shopDam.gameObject.SetActive(false);
            }
        }
    }
    public void leaveClick() //побег
    {
        leaveCh = Random.Range(1, 30);
        if (leaveCh - enemyScript.lvlEn > 0)
        {
            isLeave = true;
            enNumb = prevEn;
            StartCoroutine(enemyDead());
            etFig = 2;
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
        GetComponent<attackScript>().clickTextPool[0].StartMotionHp(playerScript.hpMax - attackScript.hpLast);
        GetComponent<attackScript>().dam.text = "Вы восстановили своё здоровье";
        if (enemyScript.hpEn >= 0)
        StartCoroutine(GetComponent<attackScript>().Defense());
 
    }
    public IEnumerator Attack() //атака врага
    {
        enhpBarShab.gameObject.SetActive(true);
        leaveBt.gameObject.SetActive(false);
        enemyScript.hpEn -= (playerScript.dam + damBon);
        dam.text = "Вы нанесли " + (playerScript.dam + damBon).ToString() + " урона!";
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
            attack.gameObject.SetActive(true);
            randMoney = Random.Range((-4 * enemyScript.lvlEn), (5 * enemyScript.lvlEn));
            moneyBon = (5 * enemyScript.lvlEn) + randMoney;
            enhpBarShab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0001f);
            xpBon = (enemyScript.lvlEn * 5);
            attack.GetComponentInChildren<Image>().sprite = butCon;
            playerScript.xp += xpBon;
            playerScript.money += moneyBon;
            clickTextMoney[0].StartMotionXp(moneyBon);
            enCur.color = new Color(255, 255, 255, 255);
            dam.text = $"Противник уничтожен! Вы получили {xpBon} опыта и {moneyBon} монет.";
            enCur.sprite = dead;
            GameCount++;
                if (playerScript.xp >= playerScript.lvlNext)
                {
                    damLast = (playerScript.dam);
                    hpLast = (int)playerScript.hp;
                    sound.PlayOneShot(coinS);
                    playerScript.lvl++;
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
            attack.gameObject.SetActive(true);
            enhpBarShab.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.0001f);
            attack.GetComponentInChildren<Image>().sprite = butCon;
            enCur.color = new Color(255, 255, 255, 255);
            dam.text = "Вы успешно убежали!";
            enCur.sprite = clear;
            GameCount++;

        }
    }
    public IEnumerator Defense() //принятие удара
    {
        inShop = false;
        attack.gameObject.SetActive(false);
        hill.gameObject.SetActive(false);
        leaveBt.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        sound.PlayOneShot(damS);
        enemyScript.damEn = (enemyScript.lvlEn * 5);
        damBon = Random.Range(-5, 5);
        playerScript.hp -= (enemyScript.damEn + damBon);
        clickTextPool[0].StartMotion(enemyScript.damEn + damBon); 
        dam.text = "Враг нанес " + (enemyScript.damEn + damBon).ToString() + " урона!";
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
            if (GameCount < 5)
            {
                enNumb = Random.Range(1, 4);
            }
            else if (GameCount >= 5 && GameCount < 10)
            {
                enNumb = Random.Range(1, 8);
            }
            else if (GameCount >= 10 && GameCount < 30)
            {
                enNumb = Random.Range(1, 11);
            }
            else if (GameCount >= 30 && GameCount < 50)
            {
                enNumb = Random.Range(1, 13);
            }
            else if (GameCount >= 50 && GameCount < 85)
            {
                enNumb = Random.Range(1, 15);
            }
            else if (GameCount >= 85)
            {
                enNumb = Random.Range(1, 16);
            }
            if (playerScript.money <= 10 && enNumb == 7)
            {
                enNumb = Random.Range(1, 7);
            }
            if (playerScript.hp == playerScript.hpMax && enNumb == 10)
            {
                enNumb = Random.Range(1, 10);
            }
        }
        switch (enNumb)
        {
            case 1:
                enemyScript.hpEnmax = 10;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 1;
                namEn.text = "Комар (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im1;
                enemyScript.item = false;
                break;
            case 2:
                enemyScript.hpEnmax = 20;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 2;
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
                enemyScript.hpEnmax = 40;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 3;
                
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
                enemyScript.hpEnmax = 50;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 4;
                namEn.text = "Крот (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im7;
                enemyScript.item = false;
                break;
            case 9:
                enemyScript.hpEnmax = 75;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 5;
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
                enemyScript.hpEnmax = 165;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 9;
                namEn.text = "Сокол (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 12:
                enemyScript.hpEnmax = 235;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 12;
                namEn.text = "Волк (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 13:
                enemyScript.hpEnmax = 500;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 15;
                namEn.text = "Рысь (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 14:
                enemyScript.hpEnmax = 700;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 18;
                namEn.text = "Тигр (" + enemyScript.lvlEn.ToString() + " lvl)";
                enCur.sprite = im9;
                enemyScript.item = false;
                break;
            case 15:
                enemyScript.hpEnmax = 950;
                enemyScript.hpEn = enemyScript.hpEnmax;
                enemyScript.lvlEn = 20;
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
