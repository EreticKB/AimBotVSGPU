using UnityEngine;

public class ControlSensitivity : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Slider _sensitivity;

    private void Awake()
    {
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out float value, 0.2f);
        _sensitivity.value = value;
    }
    public void SensitivityUpdate(float value)
    {
        SaveHandler.SaveProperty(Game.IndexControlSensitivity, _sensitivity.value);
    }
}
