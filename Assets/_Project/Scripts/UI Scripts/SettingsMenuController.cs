using UnityEngine;
using UnityEngine.UI;
public class SettingsMenuController : MenuScriptControllerRoot
{
    private Strafe _strafe;
    [SerializeField] RectTransform _point;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    [SerializeField] Slider _sensitivity;
    private void Awake()
    {
        _strafe = new Strafe(_point, _speed, _radius);
    }

    private void Update()
    {
        _strafe.UpdateFromTilting(Game.TiltOffSet);   
    }
    public void ReturnToMainMenu()
    {
        MenuTravel(0);
    }

    public void CalibrateTilt()
    {
        Vector3 inputTilting = Input.acceleration;
        Quaternion tiltOffSet = new Quaternion();
        float tempX = inputTilting.x;
        float tempZ = inputTilting.z;
        inputTilting.x = -Mathf.Asin(inputTilting.y) * Mathf.Rad2Deg;
        inputTilting.y = Mathf.Asin(tempX) * Mathf.Rad2Deg;
        inputTilting.z = 0;
        tiltOffSet = Quaternion.Euler(inputTilting);
        if (tempZ >= 0) tiltOffSet.w = -tiltOffSet.w;
        Game.SavedTiltOffSet = tiltOffSet;
        Game.TiltOffSet = tiltOffSet;
    }

    public void ChangeSensitivity()
    {
        //_sensitivity.value
    }
}
