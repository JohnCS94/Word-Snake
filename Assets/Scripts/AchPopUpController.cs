using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchPopUpController : MonoBehaviour {

    public Text buttonText;
    public Text _description;
    public Text _name;
    public Sprite _image;
    public Text _progress;
    public Text _coins;
    public Image _coin;

    public Transform _popUpPanel;

    public Achievements achScr;

    Dictionary<string, achievement> achievementName = new Dictionary<string, achievement>();
  
    achievement Pupil, Reader, Bookworm, Savant, Scholar, Bibliophole, Classic, OldSchool, Vintage, Antique, Palindrome, Quizzed, Jeux,
        Fluent, WordSmith, Articulant, Orator, Worm, Garter, Python, Anaconda;

    void Start()
    {
        Pupil = new achievement("Pupil", "Play a single game of Word Snake", null, PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 1, 5);
        Reader = new achievement("Reader", "Play 5 games of Word Snake", null, PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 5, 10);
        Bookworm = new achievement("Bookworm", "Play 10 games of Word Snake", null, PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 10, 100);
        Savant = new achievement("Savant", "Play 50 games of Word Snake", null, PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 50, 250);
        Scholar = new achievement("Scholar", "Play 100 games of Word Snake", null, PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 100, 500);
        Bibliophole = new achievement("Bibliophile", "Play 500 games of Word Snake", null, PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 500, 1000);
        Classic = new achievement("Classic", "Play a single game of Classic Snake", null, PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 1, 5);
        OldSchool = new achievement("Old School", "Play 5 games of Classic Snake", null, PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 5, 10);
        Vintage = new achievement("Vintage", "Play 50 games of Classic Snake", null, PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 50, 100);
        Antique = new achievement("Antique", "Play 250 games of Classic Snake", null, PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 250, 250);
        Palindrome = new achievement("Palindrome", "Create a word that is the same forwards as backwards", null, PlayerPrefs.GetInt("achEleven"), 1, 100);
        Quizzed = new achievement("Quizzed", "Create a word with both the letters Q and Z", null, PlayerPrefs.GetInt("achTwelve"), 1, 100);
        Jeux = new achievement("Jeux", "Create a word with both the letters J and X", null, PlayerPrefs.GetInt("achThirteen"), 1, 100);
        Fluent = new achievement("Fluent", "Create 10 Words", null, PlayerPrefs.GetInt("Total_Words"), 10, 5);
        WordSmith = new achievement("Word Smith", "Create 50 Words", null, PlayerPrefs.GetInt("Total_Words"), 50, 10);
        Articulant = new achievement("Articulant", "Create 100 Words", null, PlayerPrefs.GetInt("Total_Words"), 100, 50);
        Orator = new achievement("Orator", "Create 200 Words", null, PlayerPrefs.GetInt("Total_Words"), 200, 200);
        Worm = new achievement("Worm", "Have a snake of at least 5 tiles in Classic mode", null, PlayerPrefs.GetInt("Longest_Tail"), 5, 50);
        Garter = new achievement("Garter", "Have a snake of at least 10 tiles in Classic mode", null, PlayerPrefs.GetInt("Longest_Tail"), 10, 100);
        Python = new achievement("Python", "Have a snake of at least 20 tiles in Classic mode", null, PlayerPrefs.GetInt("Longest_Tail"), 20, 250);
        Anaconda = new achievement("Anaconda", "Have a snake of at least 50 tiles in Classic mode", null, PlayerPrefs.GetInt("Longest_Tail"), 50, 500);

        PopulateMap();
    }

    void PopulateMap()
    {
        achievementName.Add("Pupil", Pupil);
        achievementName.Add("Reader", Reader);
        achievementName.Add("Bookworm", Bookworm);
        achievementName.Add("Savant", Savant);
        achievementName.Add("Scholar", Scholar);
        achievementName.Add("Bibliophole", Bibliophole);
        achievementName.Add("Classic", Classic);
        achievementName.Add("OldSchool", OldSchool);
        achievementName.Add("Vintage", Vintage);
        achievementName.Add("Antique", Antique);
        achievementName.Add("Palindrome", Palindrome);
        achievementName.Add("Quizzed", Quizzed);
        achievementName.Add("Jeux", Jeux);
        achievementName.Add("Fluent", Fluent);
        achievementName.Add("WordSmith", WordSmith);
        achievementName.Add("Articulant", Articulant);
        achievementName.Add("Orator", Orator);
        achievementName.Add("Worm", Worm);
        achievementName.Add("Garter", Garter);
        achievementName.Add("Python", Python);
        achievementName.Add("Anaconda", Anaconda);

    }

    public struct achievement
    {
        string AchName;
        string Description;
        //Image AchImage;
        int AchTotal;
        int AchGoal;
        int coin;

        public achievement(string name, string desc, Image img, int total, int goal, int coins)
        {
            AchName = name;
            Description = desc;
            //AchImage = img;
            AchTotal = total;
            AchGoal = goal;
            coin = coins;
        }

        public string getName()
        {
            if (AchTotal < AchGoal)
                return "??????";
            else 
                return AchName;
        }

        public string getDescription()
        {
            return Description;
        }

        public Sprite getImage()
        {
            Sprite AchImage = Resources.Load<Sprite>("Achievements/" + AchName);
            Sprite lockedImg = Resources.Load<Sprite>("Achievements/AchLocked");

            if (AchTotal < AchGoal) {
                GameObject.Find("AchImg").GetComponent<SpriteRenderer>().sprite = lockedImg;
            }
            if (AchTotal >= AchGoal)
            {
                GameObject.Find("AchImg").GetComponent<SpriteRenderer>().sprite = AchImage;
            }

            return AchImage;
        }

        public int getTotal()
        {
            return AchTotal;
        }

        public int getGoal()
        {
            return AchGoal;
        }

        public string getCoins()
        {
            if (AchTotal >= AchGoal)
            {
                return "+" + coin.ToString();
            }
            else
            {
                return "";
            }
        } 
    }

    public void PopToggle()
    {
        if(_popUpPanel.gameObject.activeInHierarchy == true)
        {
            _popUpPanel.gameObject.SetActive(false);
        }
    }

    public void ReturnDetails(string achievements)
    {
        if(_popUpPanel.gameObject.activeInHierarchy == false)
        {
            _popUpPanel.gameObject.SetActive(true);
        }
        if (achievementName[achievements].getName() != "??????")
            _name.text = achievementName[achievements].getName();
        else
            _name.text = "??????";
        _description.text = achievementName[achievements].getDescription();
        _image = achievementName[achievements].getImage();
        _progress.text = achievementName[achievements].getTotal() + " / " + achievementName[achievements].getGoal();
        _coins.text = achievementName[achievements].getCoins();
        achScr.AchOne();
        if (_coins.text.Equals(""))
        {
            _coin.gameObject.SetActive(false);
        }
        else
        {
            _coin.gameObject.SetActive(true);
        }
    }
}
