using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour
{

    public Toggle targetToggle;
    public Sprite selectedSprite;
    public string pref;

    // Use this for initialization
    void Start()
    {
        targetToggle.toggleTransition = Toggle.ToggleTransition.None;
        targetToggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
        Image targetImage = targetToggle.targetGraphic as Image;

        if (targetImage != null)
        {
            if (PlayerPrefs.GetInt(pref) == 0)
            {
                targetImage.overrideSprite = selectedSprite;
            }
            else
            {
                targetImage.overrideSprite = null;
            }
        }
    }

    void OnTargetToggleValueChanged(bool newValue)
    {
        Image targetImage = targetToggle.targetGraphic as Image;
        if (targetImage != null)
        {
            if (PlayerPrefs.GetInt(pref) == 1)
            {
                targetImage.overrideSprite = selectedSprite;
                PlayerPrefs.SetInt(pref, 0);
            }
            else
            {
                targetImage.overrideSprite = null;
                PlayerPrefs.SetInt(pref, 1);
            }
        }
    }
}