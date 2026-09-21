using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class InterstitialAdManager : MonoBehaviour
{
    private const string TEST_AD_UNIT_ID = "ca-app-pub-3940256099942544/1033173712";

    private InterstitialAd interstitialAd;
    private Action onAdClosed;

    private void Start()
    {
#if UNITY_ANDROID
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("Google Mobile Ads SDK initialized.");
            LoadInterstitialAd();
        });
#endif
    }

    private void OnDestroy()
    {
        interstitialAd?.Destroy();
        interstitialAd = null;
        onAdClosed = null;
    }

    private void LoadInterstitialAd()
    {
        interstitialAd?.Destroy();
        interstitialAd = null;

        AdRequest adRequest = new();
        InterstitialAd.Load(
            TEST_AD_UNIT_ID,
            adRequest,
            (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Interstitial ad failed to load: " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded successfully.");
                interstitialAd = ad;
                RegisterAdEvents();
            });
    }

    private void RegisterAdEvents()
    {
        interstitialAd.OnAdFullScreenContentClosed += HandleAdsClosed;
        interstitialAd.OnAdFullScreenContentFailed += HandleAdFailed;
    }

    private void HandleAdsClosed()
    {
        Debug.Log("Interstitial ad closed.");
        LoadInterstitialAd();
        InvokeAfterAdClosed();
    }

    private void HandleAdFailed(AdError error)
    {
        Debug.LogError("Interstitial ad failed to show: " + error);
        LoadInterstitialAd();
        InvokeAfterAdClosed();
    }

    private void InvokeAfterAdClosed()
    {
        Action callback = onAdClosed;
        onAdClosed = null;
        callback?.Invoke();
    }

    public void ShowAd(Action onAdClosed)
    {
        this.onAdClosed = onAdClosed;
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready.");
            InvokeAfterAdClosed();
        }
    }
}
