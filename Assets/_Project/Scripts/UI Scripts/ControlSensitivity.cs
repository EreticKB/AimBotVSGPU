using UnityEngine;

public class ControlSensitivity : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Slider _sensitivity;

    private void Awake()
    {
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out float value, 0.2f);
        _sensitivity.value = 1.1f - value;
    }
    public void SensitivityUpdate(float value)
    {
        SaveHandler.SaveProperty(Game.IndexControlSensitivity, 1.1f - _sensitivity.value);
    }
}
