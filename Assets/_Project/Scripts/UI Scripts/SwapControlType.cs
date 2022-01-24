using UnityEngine;
using UnityEngine.UI;

public class SwapControlType : MonoBehaviour
{
    [SerializeField] Text _text;

    private void Awake()
    {
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out bool state, true);
        Debug.Log(state);
        _text.text = GetStateStatus(state);
    }
    public void ChangeControls()
    {
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out bool state, true);
        state = state ? false : true;
        _text.text =  GetStateStatus(state);
        SaveHandler.SaveProperty(Game.IndexTiltActivation, state);
    }

    private string GetStateStatus(bool state)
    {
        return state ? "Change to Stick" : "Change to Tilt";
    }
}
