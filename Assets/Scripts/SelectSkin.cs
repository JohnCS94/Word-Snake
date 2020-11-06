using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkin : MonoBehaviour {

    public RectTransform mustUnlock;
    public RectTransform buyCurrency;
    public GameObject selectedSprite;
    private AudioSource bought1;
    private AudioSource bought2;
    private AudioSource bought3;

    Sprite[] selected;

    void Start()
    {
        AudioSource[] select = GameObject.Find("Purchaser").GetComponents<AudioSource>();
        bought1 = select[0];
        bought2 = select[1];
        bought3 = select[2];
        mustUnlock.gameObject.SetActive(false);
        buyCurrency.gameObject.SetActive(false);

        selected = Resources.LoadAll<Sprite>("CharacterSprites/HeadFlipped");
        selectedSprite.GetComponent<Image>().sprite = (Sprite)selected[PlayerPrefs.GetInt("CurHead")];
    }

    static string _unlocked;
    static int _spriteNum;
   
    public void wrapperUnlocked(string unlocked)
    {
       _unlocked = unlocked;
    }

    public void wrapperSpriteNum(int spriteNum)
    {
        _spriteNum = spriteNum;
    }

    public void select()
    {
        if (PlayerPrefs.GetInt(_unlocked) == 0)
        {
            bought1.Play();
            unlockPopUp();
        }
        else if (PlayerPrefs.GetInt(_unlocked) == 1 ||_unlocked == null)
        {
            bought2.Play();
            PlayerPrefs.SetInt("CurHead", _spriteNum);
            selectedSprite.GetComponent<Image>().sprite = (Sprite)selected[PlayerPrefs.GetInt("CurHead")];
        }
    }

    ///////////////////////////
    public void unlockPopUp()
    {
        if (mustUnlock.gameObject.activeInHierarchy == false)
        {
            mustUnlock.gameObject.SetActive(true);
        }
        else
        {
            mustUnlock.gameObject.SetActive(false);
        }
    }

    public void buyCurrencyPopUp()
    {
        if (buyCurrency.gameObject.activeInHierarchy == false)
        {
            buyCurrency.gameObject.SetActive(true);
        }
        else
        {
            buyCurrency.gameObject.SetActive(false);
        }
    }
}
