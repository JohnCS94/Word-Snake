using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefSetter : MonoBehaviour
{
    static string classPref;

    public void ChoosePref(string pref)
    {
        classPref = pref;
    }

    public void IntSetter(int i)
    {
        PlayerPrefs.SetInt(classPref, i);
    }

    public void FloatSetter(float f)
    {
        PlayerPrefs.SetFloat(classPref, f);
    }

    public void StringSetter(string s)
    {
        PlayerPrefs.SetString(classPref, s);
    }

    public void SetIntPref(string pref, int val)
    {
        PlayerPrefs.SetInt(pref, val);
    }

    public void SetFloatPref(string pref, float val)
    {
        PlayerPrefs.SetFloat(pref, val);
    }
    
    public void SetStringPref(string pref, string val)
    {
        PlayerPrefs.SetString(pref, val);
    }

    public void InitializeIntPref(string pref, int val)
    {
        if(!PlayerPrefs.HasKey(pref))
        {
            PlayerPrefs.SetInt(pref, val);
        }
    }

    public void InitializeFloatPref(string pref, float val)
    {
        if (!PlayerPrefs.HasKey(pref))
        {
            PlayerPrefs.SetFloat(pref, val);
        }
    }

    public void InitializeStringPref(string pref, string val)
    {
        if (!PlayerPrefs.HasKey(pref))
        {
            PlayerPrefs.SetString(pref, val);
        }
    }
}
