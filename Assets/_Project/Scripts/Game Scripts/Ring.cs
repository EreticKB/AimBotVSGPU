using UnityEngine;

public class Ring : MonoBehaviour
{
    Transform _rotation;
    GameObject _gameObject;
    private byte _ringDirection;
    private float _ringSpeed;
    private float _ringOffSet;
    public int Difficulty;
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
        Quaternion.Euler(0, 0, _ringOffSet);
    }

    public void DestroyMe()
    {
        _gameObject.SetActive(false);
    }

    private void Update()
    {
        Rotation(_ringDirection, _ringSpeed, _ringOffSet);
        
    }
    private void Rotation(int direction, float speed, float offSet)
    {
       _rotation.rotation = direction == 0 ? Quaternion.Euler(0, 0, speed * Time.time + offSet) : Quaternion.Euler(0, 0, -speed * Time.time + offSet);
    }




}
