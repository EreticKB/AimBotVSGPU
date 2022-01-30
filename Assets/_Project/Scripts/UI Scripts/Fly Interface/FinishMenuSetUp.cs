using UnityEngine;
using UnityEngine.UI;

public class FinishMenuSetUp : MonoBehaviour
{
    [SerializeField] private GameObject _losingSubMenu;
    [SerializeField] private GameObject _finishSubMenu;

    public void SetUpFinish()
    {
        _finishSubMenu.SetActive(true);
        _losingSubMenu.SetActive(false);
    }
}
