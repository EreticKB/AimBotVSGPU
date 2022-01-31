//удалить когда буду чистить код.

using UnityEngine;
using UnityEngine.UI;

public class AttitudeCheck : MonoBehaviour
{
    public Vector3 _orientation;
    public Vector2 x;
    public Vector2 y;
    public Vector3 Tilt;
    private Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    public void UpdateData()
    {
        _text.text = $"1 {_orientation} \n 2 {Input.acceleration} \n {Tilt}";
    }
}
