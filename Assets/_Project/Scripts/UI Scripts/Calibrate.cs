using UnityEngine;

public class Calibrate : MonoBehaviour
{
    public void CalibrateTilt()
    {
        Vector3 inputTilting = Input.acceleration;
        Quaternion tiltOffSet = new Quaternion();
        float tempX = inputTilting.x;
        float tempZ = inputTilting.z;
        inputTilting.x = -Mathf.Asin(inputTilting.y) * Mathf.Rad2Deg;
        inputTilting.y = Mathf.Asin(tempX) * Mathf.Rad2Deg;
        inputTilting.z = 0;
        tiltOffSet = Quaternion.Euler(inputTilting);
        if (float.IsNaN(tiltOffSet.x) || float.IsInfinity(tiltOffSet.x)) return;
        if (float.IsNaN(tiltOffSet.y) || float.IsInfinity(tiltOffSet.y)) return;
        if (float.IsNaN(tiltOffSet.z) || float.IsInfinity(tiltOffSet.z)) return;
        if (float.IsNaN(tiltOffSet.w) || float.IsInfinity(tiltOffSet.w)) return;
        if (tempZ >= 0) tiltOffSet.w = -tiltOffSet.w;
        Game.SavedTiltOffSet = tiltOffSet;
        Game.TiltOffSet = tiltOffSet;
    }
}
