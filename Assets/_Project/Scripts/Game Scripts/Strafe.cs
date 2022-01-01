using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafe : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) _rigidbody.AddForce(Vector3.up * Speed);
        if (Input.GetKey(KeyCode.D)) _rigidbody.AddForce(Vector3.right * Speed);
        if (Input.GetKey(KeyCode.A)) _rigidbody.AddForce(Vector3.left * Speed);        
        if (Input.GetKey(KeyCode.S)) _rigidbody.AddForce(Vector3.down * Speed);
    }


   
}
