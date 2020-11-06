using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHighScoreBoards : MonoBehaviour {

    public Toggle targetToggle;
    public Sprite selectedSprite;
    public Text highScoreText;

	// Use this for initialization
	void Start () {
        targetToggle.toggleTransition = Toggle.ToggleTransition.None;
        targetToggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
        Image targetImage = targetToggle.targetGraphic as Image;

        if (targetImage != null)
        {
            if (PlayerPrefs.GetString("ScoresDisplay").Equals("WordSnake"))
            {
                targetImage.overrideSprite = selectedSprite;
            }
            else
            {
                targetImage.overrideSprite = null;
            }
        }
        OnTargetToggleValueChanged(true);

    }
	
    void OnTargetToggleValueChanged(bool newValue)
    {
        Image targetImage = targetToggle.targetGraphic as Image;
        if(targetImage != null)
        {
            if (PlayerPrefs.GetString("ScoresDisplay").Equals("Classic"))
            {
                targetImage.overrideSprite = selectedSprite;
                PlayerPrefs.SetString("ScoresDisplay", "WordSnake");
                setText("word");
            }

            else
            {
                targetImage.overrideSprite = null;
                PlayerPrefs.SetString("ScoresDisplay", "Classic");
                setText("classic");
            }
        }
    }

    void setText(string s)
    {
        string tempScore = "";
        if (s.Equals("word"))
        {
            tempScore = scoreExist("0HScore")
             + scoreExist("1HScore")
             + scoreExist("2HScore")
             + scoreExist("3HScore")
             + scoreExist("4HScore")
             + scoreExist("5HScore")
             + scoreExist("6HScore")
             + scoreExist("7HScore")
             + scoreExist("8HScore")
             + scoreExist("9HScore");
        }
        else if (s.Equals("classic"))
        {
            tempScore = scoreExist("0CHScore")
                 + scoreExist("1CHScore") 
                 + scoreExist("2CHScore")
                 + scoreExist("3CHScore")
                 + scoreExist("4CHScore")
                 + scoreExist("5CHScore")
                 + scoreExist("6CHScore")
                 + scoreExist("7CHScore")
                 + scoreExist("8CHScore")
                 + scoreExist("9CHScore");
        }
        highScoreText.text = "High Scores: \n" + tempScore;
    }

    private  string scoreExist (string scoreString)
    {
        int score = PlayerPrefs.GetInt(scoreString);
        
       if (score != 0)
        {
            return score.ToString() + "\n";
        }
        return "";

    }


}
