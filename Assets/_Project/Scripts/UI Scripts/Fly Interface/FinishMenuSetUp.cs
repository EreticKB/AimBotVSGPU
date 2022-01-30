using UnityEngine;

public class FinishMenuSetUp : MonoBehaviour
{
    [SerializeField] private GameObject _stillHaveTries;
    [SerializeField] private GameObject _finishSubMenu;

    public void SetUpFinish()
    {
        _finishSubMenu.SetActive(true);
        _stillHaveTries.SetActive(false);
    }
}
