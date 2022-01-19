using UnityEngine;

public class Fly : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody _rigidbody;
    bool stop;
    static Vector3 _currentSpeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) stop = stop ? false : true;
    }
    private void FixedUpdate()
    {
        _currentSpeed = _rigidbody.velocity;
        _currentSpeed.z = stop ? Speed : 0;
        _rigidbody.velocity = _currentSpeed;
    }

    public void TestShipStart()
    {
        stop = stop ? false : true;
    }
    
}
