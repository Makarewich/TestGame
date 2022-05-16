using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InitAd : MonoBehaviour
{
    private InterstitialAd inter;
    private string InterID = "ca-app-pub-3940256099942544/1033173712";
    private string BannerID = "ca-app-pub-3940256099942544/6300978111";
    private BannerView bannerView;

    private void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
        this.RequestBanner();

        List<string> deviceIds = new List<string>();
        deviceIds.Add("My");
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTestDeviceIds(deviceIds).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

    }
    private void OnEnable()
    {
        inter = new InterstitialAd(InterID);
        AdRequest adReq = new AdRequest.Builder().Build();
        inter.LoadAd(adReq);
    }
    public void ShowAdMob()
    {
        if (inter.IsLoaded()) inter.Show();
    }
    private void RequestBanner()
    {
        this.bannerView = new BannerView(BannerID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);

    }

}
