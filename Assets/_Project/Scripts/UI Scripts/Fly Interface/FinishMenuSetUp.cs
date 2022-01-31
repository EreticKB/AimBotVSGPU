using UnityEngine;

public class FinishMenuSetUp : MonoBehaviour
{
    //������������� ����� �������.
    [SerializeField] GameObject _stillHaveTries;
    [SerializeField] GameObject _finishSubMenu;

    public void SetUpFinish()
    {
        _finishSubMenu.SetActive(true);
        _stillHaveTries.SetActive(false);
    }
}
