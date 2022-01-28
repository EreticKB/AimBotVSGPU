using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    [SerializeField] private bool _isDisabledWhenTrue = false;
    private void Awake()
    {
        ChangeStatus();
    }

    public void ChangeStatus()
    {
        SaveHandler.LoadProperty(Game.IndexTiltActivation, out bool Check, true);
        gameObject.SetActive(Check == _isDisabledWhenTrue);
    }
}
