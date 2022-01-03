using UnityEngine;

public class FollowerBehaviour : MonoBehaviour, IFollower
{
    public Vector3 Offset;
    public bool IsPositionFollowed;
    public PlayerBehaviour Player;
    private Follow _follow;


    private void Awake()
    {
        _follow = new Follow(transform, Offset, IsPositionFollowed);
        Player.AddFollower(this);
    }
    public void TakeMyPosition(Vector3 vector)
    {
        _follow.Update(vector);
    }
}
