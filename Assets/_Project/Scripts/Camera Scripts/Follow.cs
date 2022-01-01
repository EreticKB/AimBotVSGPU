using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Player;
    private Transform _camera;
    public Vector3 CameraOffset;

    private void Awake()
    {
        _camera = transform;
    }
    private void Update()
    {
        _camera.position = Player.position + CameraOffset;
    }
}
