using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefToText : MonoBehaviour
{
    public Text intPref;
    public TextMesh tmIntPref;
    public bool intUpdate;
    public bool tmInt;
    public Text floatPref;
    public TextMesh tmFloatPref;
    public bool floatUpdate;
    public bool tmFloat;
    public Text stringPref;
    public TextMesh tmStringPref;
    public bool stringUpdate;
    public bool tmString;
    public string pref;

    // Update is called once per frame
    void Update()
    {
        //Conditional statements are used to avoid NullReferenceExceptions
        if (intUpdate)
            intPref.text = "" + PlayerPrefs.GetInt(pref);

        if (floatUpdate)
            floatPref.text = "" + PlayerPrefs.GetFloat(pref);

        if (stringUpdate)
            stringPref.text = "" + PlayerPrefs.GetString(pref);

        if(tmInt)
            tmIntPref.text = "" + PlayerPrefs.GetInt(pref);

    }
}
