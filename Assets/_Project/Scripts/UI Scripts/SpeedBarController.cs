using UnityEngine;
using UnityEngine.UI;

public class SpeedBarController : MonoBehaviour
{
    [SerializeField] RectTransform _speedBar;
    private float _sLerpT;
    [SerializeField]private Vector3 _arcStart;
    [SerializeField] private Vector3 _arcEnd;
    [SerializeField] private Vector3 _center;

    private void Start()
    {
        _arcEnd.x = _arcStart.x;
        _arcEnd.y = -_arcStart.y;
    }
    void Update()
    {
        float t = Mathf.Lerp(0.3419f, 0.6581f, _sLerpT);
        _speedBar.localPosition = Vector3.Slerp(_arcStart, _arcEnd, t);
        _speedBar.localPosition += _center;
    }

    internal void SetSpeedStatus(float t)
    {
        _sLerpT = t;
    }
    
}
