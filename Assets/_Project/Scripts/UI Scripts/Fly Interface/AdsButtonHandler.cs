
using UnityEngine;
using UnityEngine.UI;

public class AdsButtonHandler : MonoBehaviour
{
    //public readonly string _adUnitId = "ca-app-pub-9525013647787018/5102012234";//��� �������� ��� �������
    public readonly string _adUnitId = "ca-app-pub-3940256099942544/5224354917"; //��� ��� �������� �������, ����� ����� ����� ���� ������ ������.
    [SerializeField] Button button;
    private void OnEnable()
    {
        AdsHandler.SetAdsHandler(this);
        AdsHandler.LoadRewardedAd(_adUnitId);
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
