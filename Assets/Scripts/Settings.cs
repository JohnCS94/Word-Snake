using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject noAdsButton;
    PrefSetter pf;

    void Awake()
    {
        pf = gameObject.GetComponent<PrefSetter>();
        InitializeSettings();
    }

    void InitializeSettings()
    {
        pf.InitializeStringPref("LastLevel", "Menu");
        pf.InitializeIntPref("TutorialPlayed", 0);

        //Ads
        pf.InitializeIntPref("NoAds", 0);
        pf.InitializeIntPref("AdCount", 0);

        //Controls
        pf.InitializeStringPref("Controls", "Swipe");
        pf.InitializeStringPref("Dpad", "Right");

        //Sound
        pf.InitializeIntPref("Music", 1);
        pf.InitializeIntPref("Sound", 1);

        //Customization
        pf.InitializeIntPref("Total_Coins", 0);

        pf.InitializeIntPref("CurHead", 0);

        pf.InitializeIntPref("PlainHead_Unlocked", 1);
        pf.InitializeIntPref("BlueHead_Unlocked", 0);
        pf.InitializeIntPref("GreenHead_Unlocked", 0);
        pf.InitializeIntPref("GoldHead_Unlocked", 0);
        pf.InitializeIntPref("MagentaHead_Unlocked", 0);
        pf.InitializeIntPref("PurpleHead_Unlocked", 0);
        pf.InitializeIntPref("RedHead_Unlocked", 0);
        pf.InitializeIntPref("OrangeHead_Unlocked", 0);
        pf.InitializeIntPref("FlagHead_Unlocked", 0);

        pf.InitializeIntPref("PlainHead_Unlocked", 1);
        pf.InitializeIntPref("BlueHeadMs_Unlocked", 0);
        pf.InitializeIntPref("GreenHeadMs_Unlocked", 0);
        pf.InitializeIntPref("GoldHeadMs_Unlocked", 0);
        pf.InitializeIntPref("MagentaHeadMs_Unlocked", 0);
        pf.InitializeIntPref("PurpleHeadMs_Unlocked", 0);
        pf.InitializeIntPref("RedHeadMs_Unlocked", 0);
        pf.InitializeIntPref("OrangeHeadMs_Unlocked", 0);
        pf.InitializeIntPref("FlagHeadMs_Unlocked", 0);

        //High Scores
        pf.InitializeIntPref("0HScore", 0);
        pf.InitializeIntPref("1HScore", 0);
        pf.InitializeIntPref("2HScore", 0);
        pf.InitializeIntPref("3HScore", 0);
        pf.InitializeIntPref("4HScore", 0);
        pf.InitializeIntPref("5HScore", 0);
        pf.InitializeIntPref("6HScore", 0);
        pf.InitializeIntPref("7HScore", 0);
        pf.InitializeIntPref("8HScore", 0);
        pf.InitializeIntPref("9HScore", 0);

        //Achievements
        pf.InitializeIntPref("Total_Words", 0);
        pf.InitializeIntPref("Longest_Tail", 0);
        pf.InitializeIntPref("Total_Wordsnake_Games_Played", 0);
        pf.InitializeIntPref("Total_Classicsnake_Games_Played", 0);

        pf.InitializeIntPref("achOne", 0);
        pf.InitializeIntPref("achTwo", 0);
        pf.InitializeIntPref("achThree", 0);
        pf.InitializeIntPref("achFour", 0);
        pf.InitializeIntPref("achFive", 0);
        pf.InitializeIntPref("achSix", 0);
        pf.InitializeIntPref("achSeven", 0);
        pf.InitializeIntPref("achEight", 0);
        pf.InitializeIntPref("achNine", 0);
        pf.InitializeIntPref("achTen", 0);
        pf.InitializeIntPref("achEleven", 0);
        pf.InitializeIntPref("achTwelve", 0);
        pf.InitializeIntPref("achThirteen", 0);
        pf.InitializeIntPref("achFourteen", 0);
        pf.InitializeIntPref("achFifteen", 0);
        pf.InitializeIntPref("achSixteen", 0);
        pf.InitializeIntPref("achSeventeen", 0);
        pf.InitializeIntPref("achEighteen", 0);
        pf.InitializeIntPref("achNineteen", 0);
        pf.InitializeIntPref("achTwenty", 0);
        pf.InitializeIntPref("achTwentyOne", 0);
    }

    public void AddCoins(int amount)
    {
        pf.SetIntPref("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + amount);
    }

    public void Update()
    {
        if(PlayerPrefs.GetInt("NoAds") == 1)
            noAdsButton.SetActive(false);
    }
}
