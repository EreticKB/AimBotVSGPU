using UnityEngine;

public class Follow
{
    private Transform _follower;
    private Vector3 _offset;
    private bool _isPositionFollowed;
    private float _prewarmDistance; //от нуля до 1, определяет процент смещения от финального положения.
    public bool _closeStart;

    public Follow(Transform transform, Vector3 offset, bool isPositionFollowed, bool isCloseStart, float distance)
    {
        _follower = transform;
        _offset = offset;
        _isPositionFollowed = isPositionFollowed;
        _closeStart = isCloseStart;
        _prewarmDistance = distance;
    }
    public Follow(Transform transform, Vector3 offset, bool isPositionFollowed)
    {
        _follower = transform;
        _offset = offset;
        _isPositionFollowed = isPositionFollowed;
        _closeStart = false;
    }
    public void UpdatePosition(Vector3 followed)
    {
        if (_closeStart) followed = Vector3.Lerp(followed - _offset, followed, _prewarmDistance);
        _follower.position = _isPositionFollowed ? followed + _offset : new Vector3(_offset.x, _offset.y, followed.z + _offset.z);
    }

    public void ChangePrewarmDistance(float distance)
    {
        _prewarmDistance = distance;
        if (distance >= 1) _closeStart = false;
    }

}
