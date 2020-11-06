using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryLookup : MonoBehaviour {

    // Use this for initialization
    public TextAsset textFile;
    public bool inDic;


    static string[] textLines;
    static int numWords = 178690;

    // Creating an array large enough to house all of the string elements
    string[] wordArray = new string[178690];

    void Start()
    {
        textLines = (textFile.text.Split('\n'));
    }

    public Boolean isWord (String word)
    {
        // String being input to compare to all of the words in the dictionary
        // Change Console.Readline() when that is created
        string Guess = word;


        // Traverses through all the words to see if word is found
        // If word is found inDic turns true, else remains false
        for (int i = 0; i < numWords; i++)
        {
            if (Guess.Equals(textLines[i]))
            {
                inDic = true;
                break;
            }
        }
        return inDic;
    }
}
