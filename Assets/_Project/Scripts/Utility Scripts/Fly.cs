using UnityEngine;

public class Fly : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody _rigidbody;
    bool reverse;
    static Vector3 _currentSpeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) reverse = reverse ? false : true;
    }
    private void FixedUpdate()
    {
        _currentSpeed = _rigidbody.velocity;
        _currentSpeed.z = reverse ? Speed : -Speed;
        _rigidbody.velocity = _currentSpeed;
    }
    
}
