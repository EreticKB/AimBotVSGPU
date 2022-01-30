using UnityEngine;
public class FlyInterfaceController : MenuScriptControllerRoot
{
    [SerializeField] GameObject _levelProgressBar;
    [SerializeField] GameObject _endlessRecordStatus;
    [SerializeField] GameObject _timer;
    [SerializeField] GameObject _crashMenu;

    public void SetLevelProgressActive()
    {
        _levelProgressBar.SetActive(true);
    }
    public void SetEndlessRecordActive()
    {
        _endlessRecordStatus.SetActive(true);
    }
    public void SetStartTimer(float timer)
    {
        _timer.GetComponent<TimerPanelController>().Timer = timer;
        _timer.SetActive(true);
    }

    public void SetCrashMenuActive(bool state)
    {
        _crashMenu.SetActive(state);
    }
}
