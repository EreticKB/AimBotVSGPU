using UnityEngine;

public class FollowerBehaviour : MonoBehaviour, IFollower
{
    public Vector3 Offset;
    public bool IsPositionFollowed;
    public PlayerBehaviour Player;
    private Follow _follow;
    private bool _isFollow;
    public bool IsFollow
    {
        get => _isFollow;
        set => _isFollow = value;
    }
    [SerializeField] private string MyName;
    public string Name
    {
        get => MyName;
        set => MyName = value;
    }
    private void Awake()
    {
        _follow = new Follow(transform, Offset, IsPositionFollowed);
        Player.AddFollower(this);

    }
    public void TakeMyPosition(Vector3 vector)
    {
        _follow.UpdatePosition(vector);
    }
}
