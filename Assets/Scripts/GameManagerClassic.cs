using UnityEngine;
using UnityEngine.UI;

public class GameManagerClassic : MonoBehaviour
{
    AudioSource source;
    Animator anim;

    public Text Count;
    public Text Score;
    public Text HighScore;

    public BoardClassic tail;
    public PlayerMovement stop;
    public GameObject stick;
    private GameObject dpad;
    public GameOverInfo goi;
    public Achievements achScr;
    public AdController adCon;

    public int wordCount = 0;
    public int score = 0;
    public int displayScore;
    private int lives = 0;
    public int fontSize;

    public int newScore;
    public int oldScore;

    static AudioSource audio1;
    static AudioSource audio2;

    public bool timeStarted = false;

    void Start()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        stop = GetComponent<PlayerMovement>();
        goi = GetComponent<GameOverInfo>();
        tail = GetComponent<BoardClassic>();
        adCon = GetComponent<AdController>();
        dpad = GameObject.Find("DPad");
    }

    public void subLife()
    {
        lives--;
    }

    public void timePoints()
    {
        score++;
    }

    public void addPts()
    {
        score = score + 1 + tail.multi;
        if(timeStarted == false)
        {
            timeStarted = true;
            InvokeRepeating("timePoints", 1, 1);
        }
    }
   
    public void HUDUpdate()
    {
        Count.text = "x " + tail.blocks;
    }
    void GameOver()
    {
        stick.SetActive(false);
        if (PlayerPrefs.GetString("Controls") == "DPad")
        {
            dpad.SetActive(false);
        }
        CancelInvoke();
        SetScore();
        updateHighScore(score);
        stop.moveEnabled = false;
        lives = 0;
        int coinAdd = 5 + (score / 10);
        PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins") + coinAdd);
        PlayerPrefs.SetInt("Total_Classicsnake_Games_Played", PlayerPrefs.GetInt("Total_Classicsnake_Games_Played") + 1);
        achScr.AchSeven();
        achScr.AchEight();
        achScr.AchNine();
        achScr.AchTen();
        wordCount = 0;
        anim.SetTrigger("GameOver");
        PlayAd();

    }

    public void PlayAd()
    {
        PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);

        if (PlayerPrefs.GetInt("AdCount") == 2)
        {
            adCon.ShowInterstitialIfReady();
        }
        else if (PlayerPrefs.GetInt("AdCount") == 3)
        {
            PlayerPrefs.SetInt("AdCount", 0);
        }
    }

    public void LiveCheck()
    {
        if (lives < 0)
        {
            GameOver();
        }
    }

    public void SetScore()
    {
        goi.score = score;
    }

    void Update()
    {
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

        Score.text = "" + displayScore;
        if (lives < 0)
        {
            GameOver();
            HUDUpdate();
        }
    }

    void updateHighScore(int score)
    {
        newScore = score;

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(i + "CHScore"))
            {
                if (PlayerPrefs.GetInt(i + "CHScore") < newScore)
                {
                    oldScore = PlayerPrefs.GetInt(i + "CHScore");
                    PlayerPrefs.SetInt(i + "CHScore", newScore);
                    newScore = oldScore;
                }
            }
            else
            {
                PlayerPrefs.SetInt(i + "CHScore", newScore);
                newScore = 0;
            }
        }

        HighScore.text = "High Scores:" + "\n"
             + PlayerPrefs.GetInt("0CHScore") + "\n"
             + PlayerPrefs.GetInt("1CHScore") + "\n"
             + PlayerPrefs.GetInt("2CHScore") + "\n"
             + PlayerPrefs.GetInt("3CHScore") + "\n"
             + PlayerPrefs.GetInt("4CHScore") + "\n"
             + PlayerPrefs.GetInt("5CHScore") + "\n"
             + PlayerPrefs.GetInt("6CHScore") + "\n"
             + PlayerPrefs.GetInt("7CHScore") + "\n"
             + PlayerPrefs.GetInt("8CHScore") + "\n"
             + PlayerPrefs.GetInt("9CHScore");
    }

}
