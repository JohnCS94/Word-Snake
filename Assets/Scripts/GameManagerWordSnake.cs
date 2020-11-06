using UnityEngine;
using UnityEngine.UI;

public class GameManagerWordSnake : MonoBehaviour {
    AudioSource source;
    Animator anim;

    public Text Count;
    public Text Score;
    public Text HighScore;

    public BoardWordSnake tail;
    public DictionaryLookup text;
    public PlayerMovement stop;
    public GameObject stick;
    private GameObject dpad;
    public GameOverInfo goi;
    public Achievements achScr;
    public AdController adCon;

    public int wordCount = 0;
    public int score;
    public int displayScore;
    private int lives = 0;
    public int fontSize;

    public int newScore;
    public int oldScore;

    static AudioSource audio1;
    static AudioSource audio2;

    void Start()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        text = GetComponent<DictionaryLookup>();
        stop = GetComponent<PlayerMovement>();
        goi = GetComponent<GameOverInfo>();
        tail = GetComponent<BoardWordSnake>();
        adCon = GetComponent<AdController>();
        dpad = GameObject.Find("DPad");
    }

    public void subLife() {
        lives--;
    }
    
    public void checkIfWord() {
        if (text.isWord(tail.finalWord) == true)
        {
            PlayerPrefs.SetInt("Total_Words", PlayerPrefs.GetInt("Total_Words") + 1);
            score = score + (tail.wpc * tail.multi) + tail.blocks;
            wordCount++;
            text.inDic = false;
            tail.audio2.Play();
            tail.blocks++;
            tail.AddBlocks();
            achScr.AchEleven(tail.finalWord);
            achScr.AchTwelve(tail.finalWord);
            achScr.AchThirteen(tail.finalWord);
            achScr.AchFourteen();
            achScr.AchFifteen();
            achScr.AchSixteen();
            achScr.AchSeventeen();
        }
        else if (tail.finalWord == "") 
        {
            // Do nothing 
        } 
        else
        {
            score = score - (tail.wpc / 2);
            tail.audio1.Play();
            if (score <= 0)
            {
                score = 0;
            }
            tail.clearTail();
        }
        tail.wpc = 0;

        LiveCheck();
        setScore();
    }


    void GameOver() {
        stick.SetActive(false);
        if (PlayerPrefs.GetString("Controls") == "DPad")
        {
            dpad.SetActive(false);
        }
        stop.moveEnabled = false;
        lives = 0;
        int coinAdd = 5 + (score / 10 * 4);
        PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinAdd);
        PlayerPrefs.SetInt("Total_Wordsnake_Games_Played", PlayerPrefs.GetInt("Total_Wordsnake_Games_Played") + 1);
        wordCount = 0;
        updateHighScore(score);
        anim.SetTrigger("GameOver");
        achScr.AchOne();
        achScr.AchTwo();
        achScr.AchThree();
        achScr.AchFour();
        achScr.AchFive();
        achScr.AchSix();
        score = 0;
        PlayAd();
    }

    public void PlayAd()
    {
        PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);

        if(PlayerPrefs.GetInt("AdCount") == 2)
        {
            adCon.ShowInterstitialIfReady();
        }
        else if(PlayerPrefs.GetInt("AdCount") == 3)
        {
            PlayerPrefs.SetInt("AdCount", 0);
        }
    }

    public void LiveCheck() {
        if (lives < 0)
        {
            GameOver();
        }
    }

    void setScore() {
        goi.score = score;
    }

    void Update() {
        if (displayScore != score)
        {
            if (displayScore < score)
            {
                Score.fontSize = fontSize + 13;
                displayScore++;
            }
            else if (displayScore > score)
            {
                Score.fontSize = fontSize - 5;
                displayScore--;
            }
        }

        if (Score.fontSize != fontSize)
        {
            if (Score.fontSize > fontSize)
            {
                Score.fontSize--;
            }
            if (Score.fontSize < fontSize)
            {
                Score.fontSize++;
            }
        }

        Score.text = ""+ displayScore;
        if (lives < 0)
        {
            GameOver();
        }
    }

    void updateHighScore(int score) {
        newScore = score;

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(i+"HScore"))
            {
                if(PlayerPrefs.GetInt(i+"HScore") < newScore)
                {
                    oldScore = PlayerPrefs.GetInt(i + "HScore");
                    PlayerPrefs.SetInt(i + "HScore", newScore);
                    newScore = oldScore;
                }
            } else
            {
                PlayerPrefs.SetInt(i+"HScore", newScore);
                newScore = 0;
            }
        }

        HighScore.text = "High Scores:" +"\n"
             + PlayerPrefs.GetInt("0HScore") + "\n"
             + PlayerPrefs.GetInt("1HScore") + "\n"
             + PlayerPrefs.GetInt("2HScore") + "\n"
             + PlayerPrefs.GetInt("3HScore") + "\n"
             + PlayerPrefs.GetInt("4HScore") + "\n"
             + PlayerPrefs.GetInt("5HScore") + "\n"
             + PlayerPrefs.GetInt("6HScore") + "\n"
             + PlayerPrefs.GetInt("7HScore") + "\n"
             + PlayerPrefs.GetInt("8HScore") + "\n"
             + PlayerPrefs.GetInt("9HScore");
    }

}
