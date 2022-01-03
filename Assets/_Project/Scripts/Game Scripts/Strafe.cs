using UnityEngine;

public class Strafe : MonoBehaviour
{
    public float Speed = 1;
    public float FieldRadius;
    public Transform _transform;
    private Vector2 _lerp = new Vector2(0f, 0f);
    private static Vector3 _position;

    private void Awake()
    {
        _transform = transform;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) _lerp = moveUp(_lerp, Speed);
        if (Input.GetKey(KeyCode.S)) _lerp = moveDown(_lerp, Speed);
        if (Input.GetKey(KeyCode.A)) _lerp = moveLeft(_lerp, Speed);
        if (Input.GetKey(KeyCode.D)) _lerp = moveRight(_lerp, Speed);
        _position = _transform.position;
        _position.x = Mathf.LerpUnclamped(0, FieldRadius, _lerp.x);
        _position.y = Mathf.LerpUnclamped(0, FieldRadius, _lerp.y);
        _transform.position = _position;
    }

    private Vector2 moveUp(Vector2 lerp, float speed)
    {
        lerp += Vector2.up * speed * Time.deltaTime;
        if (lerp.sqrMagnitude > 1) return lerp.normalized;
        else return lerp;
    }
    private Vector2 moveDown(Vector2 lerp, float speed)
    {
        lerp += Vector2.down * speed * Time.deltaTime;
        if (lerp.sqrMagnitude > 1) return lerp.normalized;
        else return lerp;
    }

    private Vector2 moveRight(Vector2 lerp, float speed)
    {
        lerp += Vector2.right * speed * Time.deltaTime;
        if (lerp.sqrMagnitude > 1) return lerp.normalized;
        else return lerp;
    }
    private Vector2 moveLeft(Vector2 lerp, float speed)
    {
        lerp += Vector2.left * speed * Time.deltaTime;
        if (lerp.sqrMagnitude > 1) return lerp.normalized;
        else return lerp;
    }
}
