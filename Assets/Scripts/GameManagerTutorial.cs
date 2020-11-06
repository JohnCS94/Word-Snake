using UnityEngine;
using UnityEngine.UI;

public class GameManagerTutorial : MonoBehaviour {
    AudioSource source;
    Animator anim;

    public Text Score;

    public BoardTutorial tail;
    public DictionaryLookup text;
    public PlayerMovement stop;
    public GameObject stick;
    public TutorialManager tm;
    private GameObject dpad;

    public int wordCount = 0;
    public int score;
    public int displayScore;
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
        tail = GetComponent<BoardTutorial>();
        //tm = GetComponent<TutorialManager>();
        dpad = GameObject.Find("DPad");
    }

    
    public void checkIfWord() {
        if (tail.finalWord == "WORD")
        {
            tm.CheckSeven();
        }

        tail.wpc = 0;
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
    }

}
