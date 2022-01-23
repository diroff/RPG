using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moveText : MonoBehaviour
{
    private bool move;
    private Vector2 moveVector;

    private void Update()
    {
        if (!move) return;
        transform.Translate(moveVector * Time.deltaTime);
    }
    public void StartMotion(float damValue)
    {

        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "-" + damValue;
        moveVector = new Vector2(0, 2);
        move = true;
        GetComponent<Animation>().Play();
    }
    public void StartMotionHp(float damValue)
    {

        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "+" + damValue + " hp";
        moveVector = new Vector2(0, 2);
        move = true;
        GetComponent<Animation>().Play();
    }
    public void StartMotionXp(float xpValue)
    {
        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "+" + xpValue;
        moveVector = new Vector2(2, 0);
        move = true;
        GetComponent<Animation>().Play();
    }

}
