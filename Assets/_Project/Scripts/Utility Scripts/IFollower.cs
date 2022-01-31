using UnityEngine;

public interface IFollower
{
    //Этот интерфейс используется для объединения всех объектов, способных следовать за другими в одну группу и реализации шаблона "Наблюдатель".
    void TakeMyPosition(Vector3 vector);
    string Name { get; set; }
    bool IsFollow { get; set; }
}