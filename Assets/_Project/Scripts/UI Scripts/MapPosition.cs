using UnityEngine;

public class MapPosition : MonoBehaviour
{
    private Strafe _strafe;
    [SerializeField] RectTransform _point;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    private void Awake()
    {
        _strafe = new Strafe(_point, _speed, _radius, _loadSensitivity());
    }

    private void Update()
    {
        _strafe.UpdateFromTilting(Game.TiltOffSet);
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
