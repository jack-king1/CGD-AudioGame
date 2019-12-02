using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawbridgeLock : MonoBehaviour
{
    int codeLength = 4;
    int placeInCode;

    int firstNumber;
    int secondNumber;
    int thirdNumber;
    int fourthNumber;

    public string code = "";
    public string attempedCode;

    public GameObject Drawbridge;
    public Animator anim;

    public bool puzzleComplete = false;

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;

    public GameObject ps;
    public GameObject pit;


    void Start ()
    {
        anim = Drawbridge.GetComponent<Animator>();

        firstNumber = Random.Range(1, 7);
        secondNumber = Random.Range(1, 7);
        thirdNumber = Random.Range(1, 7);
        fourthNumber = Random.Range(1, 7);

        code = "" + firstNumber + secondNumber + thirdNumber + fourthNumber;

        codeLength = code.Length;
    }

    void CheckCode()
    {
        if (attempedCode == code)
        {
            StartCoroutine(Bridge());
        }
        else 
        {
            Debug.Log("Wrong Code");
        }
    }

    IEnumerator Bridge()
    {
        puzzleComplete = true;
        anim.Play("DrawbridgeDown");
        yield return new WaitForSeconds(0.90f);
        ps.SetActive(true);
        pit.SetActive(false);

    }

    public void SetValue(string value)
    {
        placeInCode++;

        if (placeInCode <= codeLength)
        {
            attempedCode += value;
        }

        if (placeInCode == codeLength)
        {
            CheckCode();
            attempedCode = "";
            placeInCode = 0;
        }

        if (placeInCode == 1)
        {
            light1.SetActive(true);
            light2.SetActive(false);
            light3.SetActive(false);
            light4.SetActive(false);
        }
        if (placeInCode == 2)
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(false);
            light4.SetActive(false);
        }
        if (placeInCode == 3)
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            light4.SetActive(false);
        }
        if (puzzleComplete)
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            light4.SetActive(true);
        }
        else if (placeInCode == 0)
        {
            LightReset();
        }
    }

    public void LightReset()
    {
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        light4.SetActive(false);
    }
}
