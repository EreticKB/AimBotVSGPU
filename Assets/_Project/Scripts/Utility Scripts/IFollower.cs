using UnityEngine;

public interface IFollower
{
    void TakeMyPosition(Vector3 vector);
    string Name { get; set; }
    bool IsFollow { get; set; }
}