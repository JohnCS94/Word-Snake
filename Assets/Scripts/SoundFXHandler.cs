using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXHandler : MonoBehaviour {
    AudioSource audio1;
    AudioSource audio2;
    // Use this for initialization
    void Start () {
        audio1 = this.gameObject.GetComponent<AudioSource>();      
	}

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            audio1.volume = 0.0f;
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            audio1.volume = 0.8f;
        }


        audio2 = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            audio2.volume = 0.0f;
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            audio2.volume = 0.8f;
        }

    }
}
