using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangerAchievements : MonoBehaviour
{
    public Text text;
    public string achPref;
    int isOn;
    // Start is called before the first frame update
    void Start()
    {
        isOn = PlayerPrefs.GetInt(achPref);
        TextChange();
    }

    void TextChange()
    {
        if (isOn == 0)
            text.text = "??????";
    }
}
