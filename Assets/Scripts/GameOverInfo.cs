using UnityEngine;
using UnityEngine.UI;

public class GameOverInfo : MonoBehaviour {

    public Text ScoreAddedText;
    public Text GameOverScore;

    public int score;
    int added;

	// Update is called once per frame
	void Update () {

        GameOverScore.text = "Score: " + score;

        added = 5 + (score / 10);

        if(added > 9)
             ScoreAddedText.text = "Coins: + 0" + added;

        else if(added > 99)
            ScoreAddedText.text = "Coins: + " + added;
        
        else
            ScoreAddedText.text = "Coins: + 00" + added;
    }
}
