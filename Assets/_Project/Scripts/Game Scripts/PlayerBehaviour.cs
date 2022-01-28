using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private bool _isTiltEnabled;
    
    public bl_Joystick JoyStick; //стик для любителей садомазо со стиками
    private Strafe _strafe;
    private Fly _fly;
    private bool _crashed;
    [SerializeField] float _accelerationDistance;
    [SerializeField] SpeedBarController _speedBar;
    [SerializeField] AudioSource _engine;
    [SerializeField] AudioSource _crash;
    [SerializeField] float _minSpeed;
    [SerializeField] float _maxSpeed;

    private List<IFollower> _followers = new List<IFollower>();//больше для практики шаблона, чем по необходимости, т.к. камеру оказалось проще прикрепить к игроку и сейчас используется только для 
    private int _followersIndex = -1;//того, чтобы держать "точку выхода" на постоянном удалении.
    private Transform _transform;
    public float StrafeSpeed;
    public float FieldRadius;
    public Material[] Materials; //массив используемых в препятствиях и точке выхода материалов, позволяющий контроллируемо "искажать" туннель.
    private Vector2[] _lerp; //массив для хранения векторов Strafe._lerp. Используется для управления смещением "точки выхода" в отдалении от игрока.
    private Loop _loop;

    public enum PlayerState
    {
        Play,
        Wait,
        Crush
    }
    public PlayerState CurrentPlayerState;

    private void Awake()
    {
        _crashed = false;
        _fly = GetComponent<Fly>();
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out _isTiltEnabled, true);
        for (int i = 0; i < Materials.Length; i++)
        {
            BendMaterial(Materials[i], 0f, 0f, Vector2.zero); ;
        }
        _transform = transform;
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out float sensitivity, 0.2f);
        _strafe = new Strafe(_transform, StrafeSpeed, FieldRadius, JoyStick, sensitivity);
        _loop = new Loop(0, 100, 10);
        _lerp = new Vector2[100];
    }

    private void Update()
    {
        if (RingPassed.EndlessRecord <= 100) _fly.SetShipVelocity(Mathf.Lerp(_minSpeed, _maxSpeed, RingPassed.EndlessRecord / _accelerationDistance));
        _speedBar.SetSpeedStatus(RingPassed.EndlessRecord / _accelerationDistance);
        if (CurrentPlayerState != PlayerState.Play)
        {
            _fly.ShipEngineEngage(false);
            return;
        }
        for (int i = 0; i <= _followersIndex; i++) _followers[i].TakeMyPosition(_transform.position);

        int offSet;
        if (!_isTiltEnabled)
        {
            if (Input.anyKey) _lerp[_loop.Next(out offSet)] = _strafe.UpdateFromKey();
            else return;
        }
        else
        {
            _lerp[_loop.Next(out offSet)] = _strafe.UpdateFromTilting(Game.TiltOffSet);
        }
        for (int i = 0; i < Materials.Length; i++)
        {
            BendMaterial(Materials[i], 0f, 0.0005f, _lerp[offSet]); ;
        }
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
        if (!_crashed)
        {
            _crash.Play();
            _crashed = true;
        }
        CurrentPlayerState = PlayerState.Crush;
        _fly.ShipEngineEngage(false);
    }

    public void EngadeEngine()
    {
        _fly.ShipEngineEngage(true);
        CurrentPlayerState = PlayerState.Play;
        _engine.Play();
    }
    public void DisEngageEngine()
    {
        _fly.ShipEngineEngage(false);
        CurrentPlayerState = PlayerState.Wait;
        _engine.Stop();
    }
    private void enableFollowByName(string name, bool set)
    {
        _followers[getFollowerByName(name)].IsFollow = set;
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
}
