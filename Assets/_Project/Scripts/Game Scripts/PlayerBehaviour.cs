using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Strafe _strafe;
    private List<IFollower> _followers = new List<IFollower>();
    private int _followersIndex = -1;
    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
        _strafe = new Strafe(_transform, 1f, 150f);
    }

    private void Update()
    {
        if (Input.anyKey) _strafe.Update();
        for (int i = 0; i <= _followersIndex; i++) _followers[i].TakeMyPosition(_transform.position);
    }

    public void AddFollower(IFollower follower)
    {
        _followers.Add(follower);
        _followersIndex++;
    }
    public void RemoveFollower(IFollower follower)
    {
        _followers.Remove(follower);
        _followersIndex--;
    }
}
