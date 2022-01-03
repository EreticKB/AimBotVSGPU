using UnityEngine;

public class Follow
{
    private Transform _follower;
    private Vector3 _offset;
    private bool _isPositionFollowed;

    public Follow(Transform transform, Vector3 offset, bool isPositionFollowed)
    {
        _follower = transform;
        _offset = offset;
        _isPositionFollowed = isPositionFollowed;
    }
    public void Update(Vector3 followed)
    {
        _follower.position = _isPositionFollowed ? followed + _offset : new Vector3(0, 0, followed.z + _offset.z);
    }
}
