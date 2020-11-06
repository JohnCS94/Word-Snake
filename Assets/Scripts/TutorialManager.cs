using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text popUp;
    public PlayerMovement playerMovement;
    public MenuController menuController;
    public GameObject[] panels;
    private int index = 0;
    bool paused;

    void Update()
    {
        Debug.Log(index);
        if(paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (index == 0)
        {
            CheckOne();
        }
        else if (index == 1)
        {
            StartCoroutine(CheckTwo());
        } 
        else if (index == 2)
        {
            StartCoroutine(CheckThree());
        }
        else if (index == 3)
        {
            StartCoroutine(CheckFour());
        }
        else if (index == 4)
        {
            StartCoroutine(CheckFive());
        }
        else if (index == 5)
        {
            StartCoroutine(CheckSix());
        }
    }
    void CheckOne()
    {
        if (playerMovement.upDir == true
            || playerMovement.downDir == true
            || playerMovement.leftDir == true
            || playerMovement.rightDir == true)
        {
            PlayerPrefs.SetInt("TutorialPlayed", 1);
            panels[0].SetActive(false);
            index = 1;
        }
    }

    IEnumerator CheckTwo()
    {
        yield return new WaitForSeconds(1.8f);
        paused = true;
        panels[1].SetActive(true);
    }

    IEnumerator CheckThree()
    {
        yield return new WaitForSeconds(1.5f);
        panels[2].SetActive(true);
        paused = true;
    }

    IEnumerator CheckFour()
    {
        yield return new WaitForSeconds(1.0f);
        panels[3].SetActive(true);
        paused = true;
    }

    IEnumerator CheckFive()
    {
        yield return new WaitForSeconds(2.0f);
        panels[4].SetActive(true);
        paused = true;
    }

    IEnumerator CheckSix()
    {
        yield return new WaitForSeconds(3.0f);
        panels[5].SetActive(true);
        paused = true;
    }

    public void CheckSeven()
    {
        panels[6].SetActive(true);
        paused = true;
    }

    public void Close()
    {
        paused = false;
        StopAllCoroutines();
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].activeInHierarchy)
            {
                panels[i].SetActive(false);
            }
        }
        index++; 
    }

    public void CloseSeven()
    {
        paused = false;
        menuController.LoadTutorial();
    }
}
