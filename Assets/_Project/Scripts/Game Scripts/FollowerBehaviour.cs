using System.Collections;
using UnityEngine;

public class FollowerBehaviour : MonoBehaviour, IFollower
{
    public Vector3 Offset;
    public bool IsPositionFollowed;
    public PlayerBehaviour Player;
    public bool CloseStart;    
    [Range(0, 1)] public float StartDistance; //от нуля до 1, определяет процент смещения от финального положения.
    public float PrewarmTimer; //время до набора заданной дистанции между преследующим и преследуемым.
    private Follow _follow;
    [SerializeField] private bool _isFollow;
    public bool IsFollow
    {
        get => _isFollow;
        set => _isFollow = value;
    }
    [SerializeField] private string _myName;
    public string Name
    {
        get => _myName;
        set => _myName = value;
    }
    private void Awake()
    {
        if (CloseStart) _follow = new Follow(transform, Offset, IsPositionFollowed, CloseStart, StartDistance);
        else _follow = new Follow(transform, Offset, IsPositionFollowed);
        Player.AddFollower(this);
        StartCoroutine(moveAway()); //небольшое визуальное украшение в виде отдаляющегося "выхода".
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
