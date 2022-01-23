using UnityEngine;

public class ControlSensitivity : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Slider _sensitivity;

    private void Awake()
    {
        float value;
        SaveHandler.LoadProperty(Game.IndexControlSensitivity, out value);
        _sensitivity.value = value;
    }
    public void SensitivityUpdate(float value)
    {
        SaveHandler.SaveProperty(Game.IndexControlSensitivity, _sensitivity.value);
    }
}
