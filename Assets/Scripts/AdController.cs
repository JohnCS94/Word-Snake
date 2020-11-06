using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdController : MonoBehaviour
{
    public string bannerPosition;

    public bool bannerScene;

    private string store_id = "";
    private string video_ad = "video";
    private string banner_ad = "banner";

    void Start()
    {

#if UNITY_IPHONE
        store_id = "3396144";
#elif UNITY_ANDROID
        store_id = "3396145";
#endif

        Advertisement.Initialize(store_id, false);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner_ad))
        {
            yield return new WaitForSeconds(0.5f);
        }
        if(bannerPosition == "Top")
        {
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        }
        if(bannerPosition == "Bottom")
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }

        if (bannerScene) 
        { 
            Advertisement.Banner.Show(banner_ad); 
        }
        else
        {
            Advertisement.Banner.Hide();
        }
    }

    void OnDestroy()
    {
        Advertisement.Banner.Hide(true);
    }


    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.75f);
    }

    public void ShowInterstitialIfReady()
    {
        if(Advertisement.IsReady(video_ad))
        { 
            Wait();
            Advertisement.Show(video_ad);
            Debug.Log("Ad shown");
        } 
        else
        {
            Debug.Log("Ad was not ready");
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "WordSnake" || SceneManager.GetActiveScene().name == "ClassicSnake")
        {
            Advertisement.Banner.Hide(true);
        }
    }
}
