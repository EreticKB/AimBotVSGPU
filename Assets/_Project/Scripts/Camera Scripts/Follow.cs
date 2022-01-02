using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Followed;
    public bool IsPositionFollowed;
    private Transform _follower;
    public Vector3 Offset;

    private void Awake()
    {
        _follower = transform;
    }
    private void Update()
    {
        _follower.position = IsPositionFollowed ? Followed.position + Offset : new Vector3(0, 0, Followed.position.z + Offset.z);
    }
}
