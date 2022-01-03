using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Strafe _strafe;
    private List<IFollower> _followers = new List<IFollower>();
    private int _followersIndex = -1;
    private Transform _transform;
    public float StrafeSpeed;
    public float FieldRadius;
    private void Awake()
    {
        _transform = transform;
        _strafe = new Strafe(_transform, StrafeSpeed, FieldRadius);
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
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(getFollowerByName("ShipCamera"));
    }

    private void enableFollowByName(string name, bool set)
    {
        
    }

    private int getFollowerByName(string name)
    {
        foreach (IFollower follower in _followers)
        {
            if (follower.Name().Equals(name)) return _followers.IndexOf(follower);

        }
        return -1;
    }
}
