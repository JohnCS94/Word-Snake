using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    static string sceneName;

    void Start()
    {
        if (!PlayerPrefs.HasKey("mode"))
            PlayerPrefs.SetString("mode", "main");
    }

    public void LoadScene(string sceneNames)
    {
        if (SceneManager.GetActiveScene().name == "main")
        {
            sceneName = sceneNames;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            sceneName = sceneNames;
            StartCoroutine(PlayThenLoad());
        }
    }

    IEnumerator PlayThenLoad()
    {
        if (sceneName.Equals("main")) {
            yield return new WaitForSeconds(.3f);
            SceneManager.LoadScene(PlayerPrefs.GetString("mode"));
        }
        else
        {
            yield return new WaitForSeconds(.3f);
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadTutorial()
    {
        if(PlayerPrefs.GetString("LastLevel") == "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
        else if(PlayerPrefs.GetString("LastLevel") == "SelectMode")
        {
            SceneManager.LoadScene("WordSnake");
        }
    }

    public void ToWordSnake()
    {
        if(PlayerPrefs.GetInt("TutorialPlayed") == 0)
        {
            SceneManager.LoadScene("Tutorial");
        } 
        else if (PlayerPrefs.GetInt("TutorialPlayed") == 1)
        {
            SceneManager.LoadScene("WordSnake");
        }
    }
}
