using UnityEngine;
using UnityEngine.UI;

public class AttitudeCheck : MonoBehaviour
{
    private Text _text;
    private Vector3 _orientation;
    public Vector2 x;
    public Vector2 y;
    void Start()
    {
        _text = GetComponent<Text>();
        _orientation = Input.acceleration;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    public void UpdateData()
    {
        _text.text = $"1 {_orientation} \n 2 {Input.acceleration} \n {x} - {y}";
    }
}
