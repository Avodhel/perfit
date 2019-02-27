﻿using UnityEngine;
using GoogleMobileAds.Api;

public class ReklamKontrol : MonoBehaviour
{

    InterstitialAd interstitial;

    void Start()
    {
        /*1.asama (platform kodunun eklenmesi)*/
#if UNITY_ANDROID
        string appId = "ca-app-pub-6129193859685798~2682540255";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        /*2.asama (geçiş reklamı kodunun eklenmesi)*/
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6129193859685798/6973139057"; // reklam kimliği
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test reklam kimliği
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        //string adUnitId = "unexpected_platform";
#endif

#if UNITY_ANDROID
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        /*3.asama(test reklamlarının yüklenmesi)*/
        //*3.1 Test Reklamı için
        //AdRequest request = new AdRequest.Builder()
        //.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
        //.Build();
        //interstitial.LoadAd(request);
        //*3.2 normal reklamlar için
        AdRequest request = new AdRequest.Builder()
        .Build();
        interstitial.LoadAd(request);
#endif
    }

#if UNITY_ANDROID
    public void reklamiGoster()
    {
        /*4.asama (test reklamlarının gösterilmesi)*/
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
#endif
}
