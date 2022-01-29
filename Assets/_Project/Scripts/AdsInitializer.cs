using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    public static bool IsInitialised = false;
    [SerializeField] string _gameId;
    [SerializeField] bool _isTest = true;


    private void Awake()
    {
        initializeAds();
    }

    private void initializeAds()
    {
        if (Application.platform == RuntimePlatform.Android) Advertisement.Initialize(_gameId, _isTest, this);
        else Debug.Log($"Initialization: {IsInitialised = false}");

    }

    public void OnInitializationComplete()
    {
        IsInitialised = true;
        Debug.Log($"Initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        IsInitialised = false;
        Debug.Log($"Initialization failure.");
    }


}
