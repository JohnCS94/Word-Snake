using System;
using System.Collections;
using UnityEngine;

public class PurchaseSkin : MonoBehaviour {

    public AudioSource bought1;
    public AudioSource bought2;
    public AudioSource bought3;
    public string unlockable;
	
    void Start()
    {
        AudioSource[] select = GameObject.Find("Purchaser").GetComponents<AudioSource>();
        bought1 = select[0];
        bought2 = select[1];
        bought3 = select[2];
    }
    public void setUnlockable(string unlock)
    {
        unlockable = unlock;
    }

    public void BuySkin(int cost)
    {
        if(PlayerPrefs.GetInt("Total_Coins") >= cost)
        {
            bought3.Play();
            PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") - cost);
            PlayerPrefs.SetInt(unlockable, 1);
        }
        else
        {
            bought1.Play();
        }
    }
    

}
