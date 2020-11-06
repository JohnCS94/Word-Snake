using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentScene : MonoBehaviour
{
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("LastLevel", scene.name);
        Debug.Log(scene.name);
    }
}
