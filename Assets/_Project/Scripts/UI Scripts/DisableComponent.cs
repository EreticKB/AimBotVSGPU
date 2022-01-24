using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    private void Awake()
    {
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out bool isDisabled, true);
        gameObject.SetActive(!isDisabled);
    }
}
