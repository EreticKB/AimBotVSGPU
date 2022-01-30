
using System;
using UnityEngine;
using UnityEngine.UI;

public class AdsButtonHandler : MonoBehaviour
{
    public readonly string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    [SerializeField] Button button;
    private void OnEnable()
    {
        AdsHandler.LoadRewardedAd(_adUnitId);
        AdsHandler.SetAdsHandler(this);
    }

    public void SetInteractable()
    {
        button.interactable = true;
    }

    public void ClickButton()
    {
        button.interactable = false;
        AdsHandler.ShowAd();
    }

    internal void SetUpFinish()
    {
        transform.parent.GetComponent<FinishMenuSetUp>().SetUpFinish();
    }
}
