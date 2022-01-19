using UnityEngine;
using UnityEngine.UI;

public class AttitudeCheck : MonoBehaviour
{
    private Text _text;
    private Vector3 _orientation;
    void Start()
    {
        _text = GetComponent<Text>();
        _orientation = Input.acceleration;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $"1 {_orientation} \n 2 {Input.acceleration}";
    }
}
