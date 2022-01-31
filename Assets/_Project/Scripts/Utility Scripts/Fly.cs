using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] float _speed;
    private Rigidbody _rigidbody;
    bool _fly;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ShipEngineEngage(_fly ? false : true);
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, _fly ? _speed : 0);
    }

    public bool ShipEngineEngage(bool engage) //возвращает true если статус двигателей изменился и false, если нет.
    {
        if (_fly == engage) return false;
        _fly = engage;
        return true;
    }

    public bool GetShipEngineStatus()
    {
        return _fly;
    }
    public void SetShipVelocity(float velocity)
    {
        _speed = velocity;
    }
}
