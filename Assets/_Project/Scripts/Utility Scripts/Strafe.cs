using System;
using UnityEngine;

public class Strafe
{
    //test
    public AttitudeCheck check;
    //test
    //Используются для ограничения требуемого угла наклона для смещения корабля в крайнее положение.

    private float _clamp;
    private float _correction;
    //====
    private bl_Joystick _joy;
    private float _speed;
    public float Speed
    {
        get => _speed;
        set
        {
            if (value > 3) _speed = 3;
            else if (value < 0) _speed = 0;
            else _speed = value;
        }
    }

    private float _fieldRadius;
    public float FieldRadius
    {
        get => _fieldRadius;
        set
        {
            if (value < 0) _fieldRadius = 0;
            else _fieldRadius = value;
        }
    }
    private Transform _transform;
    private Vector2 _lerp = new Vector2(0f, 0f);
    private Vector3 _position;

    public Strafe(Transform transform, float speed, float fieldRadius, bl_Joystick joy)
    {
        _transform = transform;
        Speed = speed;
        FieldRadius = fieldRadius;
        _joy = joy;
        _clamp = 0.2f;
        _correction = 1 / _clamp;
    }


    public Vector2 UpdateFromKey()
    {
        if (Input.GetKey(KeyCode.W)) _lerp = moveDown(_lerp, Speed);
        if (Input.GetKey(KeyCode.S)) _lerp = moveUp(_lerp, Speed);
        if (Input.GetKey(KeyCode.A)) _lerp = moveLeft(_lerp, Speed);
        if (Input.GetKey(KeyCode.D)) _lerp = moveRight(_lerp, Speed);
        if (_joy.isActiveAndEnabled)
        {
            if (Input.anyKey) _lerp = moveByJoy(_lerp, Speed);
        }
        MoveTransformByLerp(_lerp);
        return _lerp;
    }

    public Vector2 UpdateFromTilting(Quaternion testTilt)
    {
        Vector3 tilting = Input.acceleration;
        tilting = testTilt * tilting;
        check.Tilt = tilting;
        Vector2 tiltingX = new Vector2(tilting.x, tilting.z);
        Vector2 tiltingY = new Vector2(tilting.y, tilting.z);
        tiltingX.x = Mathf.Clamp(tiltingX.x, -_clamp, _clamp);
        tiltingY.x = Mathf.Clamp(tiltingY.x, -_clamp, _clamp);
        
        _lerp.x = Mathf.Lerp(_lerp.x, tiltingX.x * _correction, Speed * Time.deltaTime);
        _lerp.y = Mathf.Lerp(_lerp.y, tiltingY.x * _correction, Speed * Time.deltaTime);
        if (_lerp.sqrMagnitude > 1) _lerp = _lerp.normalized;
        //[тестовая передача данных]
        check.x = _lerp;
        check.y = tiltingY;
        if (check.isActiveAndEnabled) check.UpdateData();
        //[/тестовая передача данных]
        MoveTransformByLerp(_lerp);
        
        return _lerp;
    }

    private void MoveTransformByLerp(Vector2 lerp)
    {
        Vector3 position = _transform.position;
        position.x = Mathf.LerpUnclamped(0, _fieldRadius, lerp.x);
        position.y = Mathf.LerpUnclamped(0, _fieldRadius, lerp.y);
        _transform.position = position;
    }

    private Vector2 moveByJoy(Vector2 lerp, float speed)
    {
        if (_joy.Horizontal > .5f) return moveRight(lerp, speed);
        if (_joy.Horizontal < -.5f) return moveLeft(lerp, speed);
        if (_joy.Vertical > .5f) return moveUp(lerp, speed);
        if (_joy.Vertical < -.5f) return moveDown(lerp, speed);
        return lerp;
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
