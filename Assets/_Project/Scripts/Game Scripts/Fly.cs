using UnityEngine;

public class Fly : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody _rigidbody;
    bool reverse;
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
        _rigidbody.velocity = reverse ? Vector3.forward * Speed : Vector3.back * Speed;
    }
    
}
