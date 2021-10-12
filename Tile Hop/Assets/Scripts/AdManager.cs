using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    public static AdManager instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize("ca-app-pub-3229095087295045~5306439717"); 
    }


    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3229095087295045/4474566162";
        if(this.interstitial!=null)
        {
            this.interstitial.Destroy();
        }
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if(this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            Debug.Log("Interstitial AD Shown");

        }
        else
        {
            Debug.Log("Interstitial AD is not ready yet");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
