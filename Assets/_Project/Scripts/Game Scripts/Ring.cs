using UnityEngine;

public class Ring : MonoBehaviour
{
    Transform _rotation;
    GameObject _gameObject;
    private byte _ringDirection;
    private float _ringSpeed;
    private float _ringOffSet;
    public int Difficulty;

    private Quaternion _target;
    private Vector3 _targetOffSet = new Vector3(0,0,90f);



    private void Awake()
    {
        _rotation = transform;
        _gameObject = gameObject;
    }

    private void OnEnable()
    {
        _ringDirection = (byte)Random.Range(0, 2);
        _ringSpeed = Random.Range(0f+(25f*Difficulty), 50f + (25f * Difficulty));
        _ringOffSet = Random.Range(0f, 90f);
        _rotation.rotation = Quaternion.Euler(0, 0, _ringOffSet);
        //_target = Quaternion.Euler(_targetOffSet) *_rotation.rotation;
    }

    public void DestroyMe()
    {
        _gameObject.SetActive(false);
    }

    private void Update()
    {
        Rotation(_ringDirection, _ringSpeed);
        
    }
    private void Rotation(int direction, float speed)
    {
        _rotation.rotation = Quaternion.Slerp(_rotation.rotation, _target, 0.00005f*speed);
        _target = direction == 0 ? Quaternion.Euler(_targetOffSet) * _rotation.rotation: Quaternion.Euler(_targetOffSet*-1) * _rotation.rotation;
    }




}
