using UnityEngine;

public class Follow
{
    //Не Моно скрипт, для исключения его из нативного управления со стороны юнити.
    private Transform _follower; //трансформ объекта, которым управляем
    private Vector3 _offset; //Максимальное смещение относительно преследуемого объекта.
    private bool _isPositionFollowed; //Определяет, надо ли нам следовать за позицией объекта на игровом поле или же только удерживать расстояние.
    private float _prewarmDistance; //от нуля до 1, определяет процент смещения от финального положения.
    public bool _closeStart; //управление режимом регулируемого отдаления

    public Follow(Transform transform, Vector3 offset, bool isPositionFollowed, bool isCloseStart, float distance)
    {
        _follower = transform;
        _offset = offset;
        _isPositionFollowed = isPositionFollowed;
        _closeStart = isCloseStart;
        _prewarmDistance = distance;
    }
    public Follow(Transform transform, Vector3 offset, bool isPositionFollowed)
    {
        _follower = transform;
        _offset = offset;
        _isPositionFollowed = isPositionFollowed;
        _closeStart = false;
    }
    public void UpdatePosition(Vector3 followed)
    {
        if (_closeStart) followed = Vector3.Lerp(followed - _offset, followed, _prewarmDistance);
        _follower.position = _isPositionFollowed ? followed + _offset : new Vector3(_offset.x, _offset.y, followed.z + _offset.z);
    }

    public void ChangePrewarmDistance(float distance) //величина от 0 до 1, определяющая смещение между максимальным и минимальным удалением от объекта, за которым следуем.
    {
        _prewarmDistance = distance;
        if (distance >= 1) _closeStart = false;
    }

}
