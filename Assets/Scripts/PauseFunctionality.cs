using UnityEngine;

public class PauseFunctionality : MonoBehaviour {

    public Transform pausePanel;
    public Transform restartPanel;
    public Transform exitPanel;

    private GameObject dpad;


    public void Start()
    {
        dpad = GameObject.Find("DPad");
    }
    public void Pause()
    {

        if (pausePanel.gameObject.activeInHierarchy == false)
        {
            pausePanel.gameObject.SetActive(true);
            if (PlayerPrefs.GetString("Controls") == "DPad")
            {
                dpad.SetActive(false);
            }
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.gameObject.SetActive(false);
            if (PlayerPrefs.GetString("Controls") == "DPad")
            {
                dpad.SetActive(true);
            }
            Time.timeScale = 1;

        }
    }
    public void ExitMenu()
    {
        
        if (exitPanel.gameObject.activeInHierarchy == false)
        {
            exitPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            exitPanel.gameObject.SetActive(false);
            
        }
    }
    public void RestartGame()
    {
        
        if (restartPanel.gameObject.activeInHierarchy == false)
        {
            restartPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            restartPanel.gameObject.SetActive(false);
        }
    }
    public void Music()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }
    public void Sound()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
    public void TimeResume()
    {
        Time.timeScale = 1;
    }
}
