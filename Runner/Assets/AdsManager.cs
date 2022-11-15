using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4978153");
        Advertisement.AddListener(this);
    }

    // Update is called once per frame
    public void PlayAd()
    {
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
    }

    public void PlayRewardedAd()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
    }

    public void PlayBannerAd()
    {
        if (Advertisement.IsReady("Banner_Android"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner_Android");
        }
        else
        {
            StartCoroutine(RepeatPlayBannerAD());
        }
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    IEnumerator RepeatPlayBannerAD()
    {
        yield return new WaitForSeconds(1);
        PlayBannerAd();
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("ADS READY");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error:" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("ADS STARTED");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            Debug.Log("Ads finished");
        }
    }
}
