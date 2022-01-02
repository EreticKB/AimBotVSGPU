using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafe : MonoBehaviour
{
    public float Speed = 1;
    public float FieldRadius;
    //private Rigidbody _rigidbody;
    private Transform _transform;
    public List<MeshRenderer> Meshes;
    private Vector2 _lerp = new Vector2(0.5f, 0.5f);
    private float _lerpX = 0.5f;
    private float _lerpY = 0.5f;
    private static Vector3 _position;
    //private float _squareDistance;
    
    private void Awake()
    {
        //_rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        //_squareDistance = Mathf.Pow(FieldRadius, 2);
    }

    /*устарело, пока оставлю, если с лерпом все будет плохо.
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) _rigidbody.AddForce(Vector3.down * Speed);
        if (Input.GetKey(KeyCode.D)) _rigidbody.AddForce(Vector3.right * Speed);
        if (Input.GetKey(KeyCode.A)) _rigidbody.AddForce(Vector3.left * Speed);        
        if (Input.GetKey(KeyCode.S)) _rigidbody.AddForce(Vector3.up * Speed);
        if (Input.GetKeyDown(KeyCode.Escape)) DestructionAnimation.DestroyEffects(_audioSource);
    }*/

    public void Update()
    {
        _position = _transform.position;
        _position.x = Mathf.Lerp(-FieldRadius, FieldRadius, _lerpX);
        _position.y = Mathf.Lerp(-FieldRadius, FieldRadius, _lerpY);
        _transform.position = _position;
        if (Input.GetKey(KeyCode.W)) moveUp(_position);
        if (Input.GetKey(KeyCode.S)) moveDown(_position);
        if (Input.GetKey(KeyCode.A)) moveLeft(_position);
        if (Input.GetKey(KeyCode.D)) moveRight(_position);
    }

    private void moveUp(Vector3 position)
    {
        /*if (((position.x * position.x) + (position.y * position.y)) >= _squareDistance && position.y > 0) return;
        _lerpY += Speed * Time.deltaTime;
        if (_lerpY > 1) _lerpY = 1;*/
    }
    private void moveDown(Vector3 position)
    {
        /*if (((position.x * position.x) + (position.y * position.y)) >= _squareDistance && position.y < 0) return;
        _lerpY -= Speed * Time.deltaTime;
        if (_lerpY < 0) _lerpY = 0;*/
    }
    
    private void moveRight(Vector3 position)
    {
        /*if (((position.x * position.x) + (position.y * position.y)) >= _squareDistance && position.x > 0) return;
        _lerpX += Speed * Time.deltaTime;
        if (_lerpX > 1) _lerpX = 1;*/
    }
    private void moveLeft(Vector3 position)
    {
        /*if (((position.x * position.x) + (position.y * position.y)) >= _squareDistance && position.x < 0) return;
        _lerpX -= Speed * Time.deltaTime;
        if (_lerpX < 0) _lerpX = 0;*/
    }
}
