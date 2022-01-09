using UnityEngine;

public class Ring : MonoBehaviour
{
    private Transform _transform;
    private GameObject _gameObject;
    private byte _ringDirection;
    private float _ringSpeed;
    private float _ringOffSet;
    [SerializeField]private int _difficulty;

    private Quaternion _target;
    private Vector3 _targetOffSet = new Vector3(0,0,90f);



    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Rotation(_ringDirection, _ringSpeed);
        
    }
    private void Rotation(int direction, float speed)
    {
        _transform.rotation = Quaternion.Slerp(_transform.rotation, _target, 0.00005f*speed);
        _target = direction == 0 ? Quaternion.Euler(_targetOffSet) * _transform.rotation: Quaternion.Euler(_targetOffSet*-1) * _transform.rotation;
    }

    public void SetUpRing(int difficulty)
    {
        _difficulty = difficulty;
        _ringDirection = (byte)Random.Range(0, 2);
        _ringSpeed = Random.Range(0f + (25f * _difficulty), 50f + (25f * _difficulty));
        _ringOffSet = Random.Range(0f, 90f);
        _transform.rotation = Quaternion.Euler(0, 0, _ringOffSet);
    }





}
