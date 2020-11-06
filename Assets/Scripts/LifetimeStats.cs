using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifetimeStats : MonoBehaviour
{
    public TextMeshProUGUI wsGames;
    public TextMeshProUGUI cGames;
    public TextMeshProUGUI wordsSpelled;
    public TextMeshProUGUI longestTail;

    void Start()
    {
        wsGames.text = "" + PlayerPrefs.GetInt("Total_Wordsnake_Games_Played");
        cGames.text = "" + PlayerPrefs.GetInt("Total_Classicsnake_Games_Played");
        wordsSpelled.text = "" + PlayerPrefs.GetInt("Total_Words");
        longestTail.text = "" + PlayerPrefs.GetInt("Longest_Tail");

    }
}
