using UnityEngine;
public class FlyInterfaceController : MenuScriptControllerRoot
{
    [SerializeField] GameObject _levelProgressBar;
    [SerializeField] GameObject _endlessRecordStatus;
    [SerializeField] GameObject _timer;
    [SerializeField] GameObject _defeatMenu;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha0)) _timer.
    }

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
    public void SetDefeatMenuActive()
    {
        _defeatMenu.SetActive(true);
    }
}
