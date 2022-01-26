using System.Collections;
using UnityEngine;

public class FollowerBehaviour : MonoBehaviour, IFollower
{
    public Vector3 Offset;
    public bool IsPositionFollowed;
    public PlayerBehaviour Player;
    private Follow _follow;
    public bool CloseStart;
    public float PrewarmTimer; //время до набора заданной дистанции между преследующим и преследуемым.
    [Range(0, 1)] public float StartDistance; //от нуля до 1, определяет процент смещения от финального положения.

    [SerializeField] private bool _isFollow;
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
        if (CloseStart) _follow = new Follow(transform, Offset, IsPositionFollowed, CloseStart, StartDistance);
        else _follow = new Follow(transform, Offset, IsPositionFollowed);
        Player.AddFollower(this);
        StartCoroutine(moveAway());
    }
    public void TakeMyPosition(Vector3 vector)
    {
        if (!_isFollow) return;
        _follow.UpdatePosition(vector);
    }
    IEnumerator moveAway()
    {
        float timer = PrewarmTimer;
        while (timer > 0)
        {
            _follow.ChangePrewarmDistance(Mathf.Lerp(StartDistance, 1, 1-timer/PrewarmTimer));
            timer -= Time.deltaTime;
            yield return null;
        }
        _follow.ChangePrewarmDistance(1);
    }
}
