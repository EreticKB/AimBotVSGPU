using UnityEngine;

public class Fly : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rigidbody;
    bool stop;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ShipEngineEngage(stop ? false : true);
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, 0, stop ? Speed : 0);
    }

    public void ShipEngineEngage(bool engage)
    {
        stop = engage;
    }
    
}
