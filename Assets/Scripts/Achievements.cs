using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {

    public Animator anim;
    public PrefSetter pf;

    public Text title;
    public Text desc;
    public Text coins;
    public SpriteRenderer img;

    // Use this for initialization
    void Start() {
        pf = GetComponent<PrefSetter>();
        SetAchievements();
    }

    public void InGameAchievement(string achievements)
    {
        title.text = achievementName[achievements].getName(); ;
        desc.text = achievementName[achievements].getDescription();
        coins.text = achievementName[achievements].getCoins();
        img.sprite = achievementName[achievements].getImage();

        anim.SetTrigger("Achievement");
    }

    private void totalWSGamesChecker(string name, string achPref, int goal, int coinsAdded)
    {
        if(PlayerPrefs.GetInt(achPref) == 0)
        {
            if (PlayerPrefs.GetInt("Total_Wordsnake_Games_Played") > goal)
            {
                InGameAchievement(name);
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinsAdded);
                PlayerPrefs.SetInt(achPref, 1);
            }
        }
    }
    private void totalCGamesChecker(string name, string achPref, int goal, int coinsAdded)
    {
        if (PlayerPrefs.GetInt(achPref) == 0)
        {
            if (PlayerPrefs.GetInt("Total_Classicsnake_Games_Played") > goal)
            {
                InGameAchievement(name);
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinsAdded);
                PlayerPrefs.SetInt(achPref, 1);
            }
        }
    }

    private void contains(string name, string word, string pref, string s1, string s2, int coinsAdded)
    {
        if (PlayerPrefs.GetInt(pref) == 0)
        {
            if (word.Contains(s1) && word.Contains(s2))
            {
                InGameAchievement(name);
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinsAdded);
                PlayerPrefs.SetInt(pref, 1);
            }
        }
    }

    private void totalWordsChecker(string name, string pref, int goal, int coinsAdded)
    {
        if (PlayerPrefs.GetInt(pref) == 0)
        {
            if (PlayerPrefs.GetInt("Total_Words") > goal)
            {
                InGameAchievement(name);
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinsAdded);
                PlayerPrefs.SetInt(pref, 1);
            }
        }
    }

    private void longestTailChecker(string name, int tailLength, string pref, int goal, int coinsAdded)
    {
        if (PlayerPrefs.GetInt(pref) == 0)
        {
            if (tailLength > goal)
            {
                InGameAchievement(name);
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinsAdded);
                PlayerPrefs.SetInt(pref, 1);
            }
        }
    }

    public void AchOne()
    {
        totalWSGamesChecker("Pupil", "achOne", 0, 5);
    }

    public void AchTwo()
    {
        totalWSGamesChecker("Reader", "achTwo", 4, 10);
    }

    public void AchThree()
    {
        totalWSGamesChecker("Bookworm", "achThree", 9, 50);
    }

    public void AchFour()
    {
        totalWSGamesChecker("Savant", "achFour", 49, 100);
    }

    public void AchFive()
    {
        totalWSGamesChecker("Scholar", "achFive", 99, 500);
    }

    public void AchSix()
    {
        totalWSGamesChecker("Bibliophile", "achSix", 499, 1000);
    }

    public void AchSeven()
    {
        totalCGamesChecker("Classic", "achSeven", 0, 5);
    }

    public void AchEight()
    {
        totalCGamesChecker("Old School", "achEight", 4, 10);
    }

    public void AchNine()
    {
        totalCGamesChecker("Vintage", "achNine", 49, 50);
    }

    public void AchTen()
    {  
        totalCGamesChecker("Antique", "achTen", 249, 500);
    }

    public void AchEleven(string str)
    {
        if (PlayerPrefs.GetInt("achEleven") == 0)
        {
            int i = 0;
            int j = str.Length - 1;
            bool palindrome = true;
            while (i < j)
            {
                if (str[i] != str[j])
                {
                    palindrome = false;
                } 
                i++;
                j--;
            }

            if (palindrome)
            {
                InGameAchievement("Palindrome");
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + 50);
                PlayerPrefs.SetInt("achEleven", 1);
            }
        }
    }

    public void AchTwelve(string containsQZ)
    {
        contains("Quizzed", containsQZ, "achTwelve", "Q", "Z", 50);
    }

    public void AchThirteen(string containsJX)
    {
        contains("Jeux", containsJX, "achThirteen", "J", "X", 50);
    }

    public void AchFourteen()
    {
        totalWordsChecker("Fluent", "achFourteen", 9, 50);
    }

    public void AchFifteen()
    {
        totalWordsChecker("Word Smith", "achFifteen", 49, 100);
    }

    public void AchSixteen()
    {
        totalWordsChecker("Articulant", "achSixteen", 99, 500);
    }

    public void AchSeventeen()
    {
        totalWordsChecker("Orator", "achSeventeen", 199, 1000);
    }

    public void AchEighteen(int tailLength)
    {
        longestTailChecker("Worm", tailLength, "achEighteen", 4, 50);
    }

    public void AchNineteen(int tailLength)
    {
        longestTailChecker("Garter", tailLength, "achNineteen", 9, 100);
    }

    public void AchTwenty(int tailLength)
    {
        longestTailChecker("Python", tailLength, "achTwenty", 19, 250);
    }

    public void AchTwentyOne(int tailLength)
    {
        longestTailChecker("Anaconda", tailLength, "achTwentyOne", 49, 500);
    }

    Dictionary<string, achievement> achievementName = new Dictionary<string, achievement>();

    achievement Pupil, Reader, Bookworm, Savant, Scholar, Bibliophole, Classic, OldSchool, Vintage, Antique, Palindrome, Quizzed, Jeux,
        Fluent, WordSmith, Articulant, Orator, Worm, Garter, Python, Anaconda;

    void SetAchievements()
    {
        Pupil = new achievement("Pupil", "Play a single game of Word Snake", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 1, 5);
        Reader = new achievement("Reader", "Play 5 games of Word Snake", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 5, 10);
        Bookworm = new achievement("Bookworm", "Play 10 games of Word Snake", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 10, 100);
        Savant = new achievement("Savant", "Play 50 games of Word Snake", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 50, 250);
        Scholar = new achievement("Scholar", "Play 100 games of Word Snake", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 100, 500);
        Bibliophole = new achievement("Bibliophile", "Play 500 games of Word Snake", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played"), 500, 1000);
        Classic = new achievement("Classic", "Play a single game of Classic Snake", PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 1, 5);
        OldSchool = new achievement("Old School", "Play 5 games of Classic Snake", PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 5, 10);
        Vintage = new achievement("Vintage", "Play 50 games of Classic Snake", PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 50, 100);
        Antique = new achievement("Antique", "Play 250 games of Classic Snake", PlayerPrefs.GetInt("Total_Classicsnake_Games_Played"), 250, 250);
        Palindrome = new achievement("Palindrome", "Create a word that is the same forwards as backwards", PlayerPrefs.GetInt("achEleven"), 1, 100);
        Quizzed = new achievement("Quizzed", "Create a word with both the letters Q and Z", PlayerPrefs.GetInt("achTwelve"), 1, 100);
        Jeux = new achievement("Jeux", "Create a word with both the letters J and X", PlayerPrefs.GetInt("achThirteen"), 1, 100);
        Fluent = new achievement("Fluent", "Create 10 Words", PlayerPrefs.GetInt("Total_Words"), 10, 5);
        WordSmith = new achievement("Word Smith", "Create 50 Words", PlayerPrefs.GetInt("Total_Words"), 50, 10);
        Articulant = new achievement("Articulant", "Create 100 Words", PlayerPrefs.GetInt("Total_Words"), 100, 50);
        Orator = new achievement("Orator", "Create 200 Words", PlayerPrefs.GetInt("Total_Words"), 200, 200);
        Worm = new achievement("Worm", "Have a snake of at least 5 tiles in Classic mode", PlayerPrefs.GetInt("Longest_Tail"), 5, 50);
        Garter = new achievement("Garter", "Have a snake of at least 10 tiles in Classic mode", PlayerPrefs.GetInt("Longest_Tail"), 10, 100);
        Python = new achievement("Python", "Have a snake of at least 20 tiles in Classic mode", PlayerPrefs.GetInt("Longest_Tail"), 20, 250);
        Anaconda = new achievement("Anaconda", "Have a snake of at least 50 tiles in Classic mode", PlayerPrefs.GetInt("Longest_Tail"), 50, 500);

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
        achievementName.Add("Old School", OldSchool);
        achievementName.Add("Vintage", Vintage);
        achievementName.Add("Antique", Antique);
        achievementName.Add("Palindrome", Palindrome);
        achievementName.Add("Quizzed", Quizzed);
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
        int AchTotal;
        int AchGoal;
        int coin;

        public achievement(string name, string desc, int total, int goal, int coins)
        {
            AchName = name;
            Description = desc;
            AchTotal = total;
            AchGoal = goal;
            coin = coins;
        }

        public string getName()
        {
            return AchName;
        }

        public string getDescription()
        {
            return Description;
        }

        public Sprite getImage()
        {
            Sprite AchImage = Resources.Load<Sprite>("Achievements/" + AchName);

            return AchImage;
        }

        public string getCoins()
        {
            return "+" + coin.ToString();
        }
    }
}
