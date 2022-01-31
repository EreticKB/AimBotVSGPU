using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] JoystickController JoyStick; //стик для любителей садомазо со стиками
    [SerializeField] float _accelerationDistance; //сколько колец надо пройти прежде чем скорость достигнет максимального значения.
    [SerializeField] SpeedBarController _speedBar;
    [SerializeField] AudioSource _engine;
    [SerializeField] AudioSource _crash;
    [SerializeField] float _minSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float StrafeSpeed;
    [SerializeField] float FieldRadius;
    [SerializeField] Material[] Materials; //массив используемых в препятствиях и точке выхода материалов, позволяющий контроллируемо "искажать" туннель.
    private bool _isTiltEnabled;
    private Strafe _strafe;
    private Fly _fly;
    private bool _crashed;
    private float _endGameAcceleration = 0;
    private List<IFollower> _followers = new List<IFollower>();//больше для практики шаблона, чем по необходимости, т.к. камеру оказалось проще прикрепить к игроку и сейчас используется только для 
    private int _followersIndex = -1;//того, чтобы держать "точку выхода" на постоянном удалении.
    private Transform _transform;
    private Vector2[] _lerp; //массив для хранения векторов Strafe._lerp. Используется для управления смещением "точки выхода" в отдалении от игрока.
    private Loop _loop;

    public enum PlayerState
    {
        Play,
        Wait,
        Crush,
        ExitSequence
    }
    public PlayerState CurrentPlayerState; //убедиться, что в юнити выставлено Wait;

    private void Awake()
    {
        _transform = transform;
        _loop = new Loop(0, 100, 10);
        _lerp = new Vector2[100];
        _accelerationDistance = _accelerationDistance == 0 ? 1 : _accelerationDistance; //если не настроили и там подставляется 0, чтобы избежать ошибки.
        _crashed = false;
        _fly = GetComponent<Fly>();
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out _isTiltEnabled, true);
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out float sensitivity, 0.2f);
        _strafe = new Strafe(_transform, StrafeSpeed, FieldRadius, JoyStick, sensitivity);
        for (int i = 0; i < Materials.Length; i++) BendMaterial(Materials[i], 0f, 0f, Vector2.zero);
    }

    private void Update()
    {

        for (int i = 0; i <= _followersIndex; i++) _followers[i].TakeMyPosition(_transform.position);
        int offSet;
        if (CurrentPlayerState == PlayerState.Play)
        {
            if (RingPassed.EndlessRecord <= _accelerationDistance) _fly.SetShipVelocity(Mathf.Lerp(_minSpeed, _maxSpeed, RingPassed.EndlessRecord / _accelerationDistance));
            _speedBar.SetSpeedStatus(RingPassed.EndlessRecord / _accelerationDistance);
            if (!_isTiltEnabled)
            {
                if (Input.anyKey) _lerp[_loop.Next(out offSet)] = _strafe.UpdateFromKey();
                else return;
            }
            else _lerp[_loop.Next(out offSet)] = _strafe.UpdateFromTilting(Game.TiltOffSet);
        }
        else if (CurrentPlayerState == PlayerState.ExitSequence)
        {
            _speedBar.SetSpeedStatus(_endGameAcceleration);
            _endGameAcceleration = _endGameAcceleration < 1 ? _endGameAcceleration + Time.deltaTime : 1;
            _fly.SetShipVelocity(Mathf.Lerp(0f, _maxSpeed * 3, _endGameAcceleration));
            _lerp[_loop.Next(out offSet)] = _strafe.MoveToCenter();
        }
        else return;
        if (CurrentPlayerState != PlayerState.Wait)for (int i = 0; i < Materials.Length; i++) BendMaterial(Materials[i], 0f, 0.00025f, _lerp[offSet]);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!_crashed)
        {
            _crash.Play();
            _crashed = true;
        }
        CurrentPlayerState = PlayerState.Crush;
    }

    public void EngadeEngine()
    {
        _fly.ShipEngineEngage(true);
        CurrentPlayerState = PlayerState.Play;
        _engine.Play();
    }
    public bool DisEngageEngine()//пробрасывает результат дальше.
    {
        bool check = _fly.ShipEngineEngage(false);
        CurrentPlayerState = PlayerState.Wait;
        _engine.Stop();
        return check;
    }
    internal void SetEnd()
    {
        CurrentPlayerState = PlayerState.ExitSequence;
        enableFollowByName("WarpTunnel", false);
        _fly.ShipEngineEngage(true);
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
    private void enableFollowByName(string name, bool set)
    {
        _followers[getFollowerByName(name)].IsFollow = set;
    }

    public void ResetPosition(float moveBack)
    {
        transform.position = new Vector3(0, 0, transform.position.z - moveBack);
        _strafe.ResetPosition();
        StartCoroutine(SmoothBendToZero());
    }

    private int getFollowerByName(string name)
    {
        foreach (IFollower follower in _followers)
        {
            if (follower.Name.Equals(name)) return _followers.IndexOf(follower);

        }
        return -1;
    }

    private void BendMaterial(Material material, float minLimit, float maxLimit, Vector2 bend)
    {
        BendingShaderController.TrySetHorizontalBending(material, -Mathf.LerpUnclamped(minLimit, maxLimit, bend.x));
        BendingShaderController.TrySetVerticalBending(material, -Mathf.LerpUnclamped(minLimit, maxLimit, bend.y));
    }

    IEnumerator SmoothBendToZero()
    {
        float time = 0;
        while (time < 1)
        {
            for (int i = 0; i < Materials.Length; i++)
            {
                BendMaterial(Materials[i], 0f, 0.00025f, Vector2.Lerp(_lerp[_loop.Current()], new Vector2(0,0), time));
            }
            time += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < _lerp.Length; i++) _lerp[i] = new Vector2(0, 0);
    }
}
