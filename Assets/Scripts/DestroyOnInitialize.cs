using System;
using System.Collections;
using UnityEngine;

public class DestroyOnInitialize : MonoBehaviour {
    
    public new GameObject gameObject;
    public string prefId;

    void Update()
    {
        if (PlayerPrefs.GetInt(prefId) == 1)
        {
            Destroy(gameObject);
        }
    }
}
