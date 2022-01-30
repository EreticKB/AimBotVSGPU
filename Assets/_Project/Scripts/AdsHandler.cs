using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsHandler : MonoBehaviour
{
    private static RewardedAd _rewardedAd;
    private static AdsButtonHandler _buttonHandler;
    private static Game _game;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public static void LoadRewardedAd(string adUnitId)
    {
        _rewardedAd = new RewardedAd(adUnitId);
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        _rewardedAd.OnAdLoaded += HandlerRewardedAdLoaded;
        _rewardedAd.OnAdClosed += HandlerRewardedAdClosed;
        _rewardedAd.OnUserEarnedReward += HandlerUserReward;
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }

    private static void HandlerUserReward(object sender, Reward e)
    {
        _game.UseContinue();
        _buttonHandler.SetUpFinish();
    }

    private static void HandlerRewardedAdClosed(object sender, EventArgs e)
    {

    }

    public static void SetAdsHandler(AdsButtonHandler handler)
    {
        _buttonHandler = handler;
    }
    public static void SetGame(Game game)
    {
        _game = game;
    }
    public static void ShowAd()
    {
        _rewardedAd.Show();
    }

    private static void HandlerRewardedAdLoaded(object sender, EventArgs e)
    {
        _buttonHandler.SetInteractable();
    }

    private static void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log($"Failure Message: {args.LoadAdError.GetMessage()}");
    }
}
