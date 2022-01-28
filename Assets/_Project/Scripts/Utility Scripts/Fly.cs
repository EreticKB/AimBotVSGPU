using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;
    bool _stop;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ShipEngineEngage(_stop ? false : true);
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, _stop ? _speed : 0);
    }

    public void ShipEngineEngage(bool engage)
    {
        _stop = engage;
    }

    public bool GetShipEngineStatus()
    {
        return _stop;
    }
    public void SetShipVelocity(float velocity)
    {
        _speed = velocity;
    }
}
