using System;
using UnityEngine;

public class Strafe
{
    //»спользуютс€ дл€ ограничени€ требуемого угла наклона дл€ смещени€ корабл€ в крайнее положение.
    private float _clamp;
    private float _correction;
    //=======================================
    private JoystickController _joy; //—сылка на виртуальный стик дл€ любителей садомазо со стиками.
    private float _speed; //ќпределе€ет скорость смещени€.
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

    private float _fieldRadius; //определ€ет радиус "игрового" пол€.
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
    private Vector2 _lerp = new Vector2(0f, 0f); //вектор, чьи составл€ющие используютс€ как t смещени€ через Mathf.Lerp().

    public Strafe(Transform transform, float speed, float fieldRadius, JoystickController joy, float sensitivity)
    {
        _transform = transform;
        Speed = speed;
        FieldRadius = fieldRadius;
        _joy = joy;
        _clamp = sensitivity;
        _correction = 1 / _clamp;
    }


    public Strafe(RectTransform transform, float speed, float fieldRadius, float sensitivity)
    {
        //дл€ использовани€ класса без джойстика, например дл€ экрана отслеживающего положение телефона в меню калибровки положени€
        _transform = transform;
        Speed = speed;
        FieldRadius = fieldRadius;
        _joy = null;
        _clamp = sensitivity;
        _correction = 1 / _clamp;
    }

    public void ChangeSensitivity(float sensitivity)
    {
        //корректировка чувствительности после создани€ класса.
        _clamp = sensitivity;
        _correction = 1 / _clamp;
    }


    public Vector2 UpdateFromKey()
    {
        //Ѕлок управлени€ с клавиатуры, оставл€ю дл€ нужд тестировани€ и как бонус дл€ тех, кто подключит клаву.(прикрутить включение режима)
        if (Input.GetKey(KeyCode.W)) _lerp = moveDown(_lerp, Speed);
        if (Input.GetKey(KeyCode.S)) _lerp = moveUp(_lerp, Speed);
        if (Input.GetKey(KeyCode.A)) _lerp = moveLeft(_lerp, Speed);
        if (Input.GetKey(KeyCode.D)) _lerp = moveRight(_lerp, Speed);
        //=============================================
        if (_joy != null) if (_joy.isActiveAndEnabled) if (Input.anyKey) _lerp = updateByJoy(_lerp);
        MoveTransformByLerp(_lerp);
        return _lerp;
    }

    public Vector2 UpdateFromTilting(Quaternion tiltOffSet)
    {
        Vector3 tilting = Input.acceleration;
        tilting = tiltOffSet * tilting;
        tilting.x = Mathf.Clamp(tilting.x, -_clamp, _clamp);
        tilting.y = Mathf.Clamp(tilting.y, -_clamp, _clamp);
        _lerp = moveBetweenPoints(_lerp, tilting, _correction);
        return _lerp;
    }
    private Vector2 updateByJoy(Vector2 lerp)
    {

        if (!_joy.GetDelta2Normalized(out Vector2 lerpTarget)) lerp = moveBetweenPoints(lerp, lerpTarget, 1);
        return lerp;
    }

    public Vector2 MoveToCenter()
    {
        _lerp = moveBetweenPoints(_lerp, Vector3.zero, 1);
        return _lerp;
    }
    private Vector3 moveBetweenPoints(Vector3 start, Vector3 end, float correction)
    {
        start = Vector2.Lerp(start, end * correction, Speed * Time.deltaTime);
        if (start.sqrMagnitude > 1) start = start.normalized;
        MoveTransformByLerp(start);
        return start;
    }
    private void MoveTransformByLerp(Vector2 lerp)
    {
        Vector3 position = _transform.localPosition;
        position.x = Mathf.LerpUnclamped(0, _fieldRadius, lerp.x);
        position.y = Mathf.LerpUnclamped(0, _fieldRadius, lerp.y);
        _transform.localPosition = position;
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
    public void ResetPosition()
    {
        _lerp = Vector2.zero;
    }
}
