using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, UnityEngine.Advertisements.IUnityAdsListener
{
    string GameID = "4403385";
    public static AdManager instance;

    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }


        Advertisement.AddListener(this);
        Advertisement.Initialize(GameID, true);
    }
  
    public void ShowAd()
    {
        if (Advertisement.IsReady("Android_Interstitial"))
            Advertisement.Show("Android_Interstitial");
        else
            Debug.Log("Ad Not ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"{message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
             
                break;
            case ShowResult.Skipped:
                break;
                
            case ShowResult.Failed:
               
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Debug.Log("Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("Ad Ready");
    }
}
