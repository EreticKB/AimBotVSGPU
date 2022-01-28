using UnityEngine;

public class MapPosition : MonoBehaviour
{
    private Strafe _strafe;
    [SerializeField] JoystickController _joyStick = null;
    [SerializeField] RectTransform _point;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    private void Awake()
    {
        if (_joyStick == null) _strafe = new Strafe(_point, _speed, _radius, _loadSensitivity());
        else _strafe = new Strafe(_point, _speed, _radius, _joyStick, _loadSensitivity());
    }

    private void Update()
    {
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out bool state, true);
        if (state) _strafe.UpdateFromTilting(Game.TiltOffSet);
        else _strafe.UpdateFromKey();
    }
    private void FixedUpdate()
    {
        _strafe.ChangeSensitivity(_loadSensitivity());
    }

    private static float _loadSensitivity()
    {
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out float value, 0.2f);
        return value;
    }
}
