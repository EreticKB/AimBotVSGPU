using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject[] _canvas;
    MenuScriptControllerRoot[] _menuController;
    public AudioSource ButtonClick;

    private void Start()
    {
        _menuController = new MenuScriptControllerRoot[_canvas.Length];
        for (int i = 0; i < _canvas.Length; i++) _menuController[i] = _canvas[i].GetComponent<MenuScriptControllerRoot>();
    }
    public void ActivateInterfaceByIndex(int index)
    {
        for (int i = 0; i < _canvas.Length; i++) _canvas[i].SetActive(i == index ? true : false);
    }

    private void LateUpdate()
    {
        //переключение между экранами меню. Данный подход позволит не создавать зависимость контроллеров различных меню от переключающего их контроллера.
        for (int i = 0; i < _menuController.Length; i++) if (_menuController[i].isActiveAndEnabled) if (_menuController[i].IsChangeNeeded) ActivateInterfaceByIndex(_menuController[i].NextMenuIndex);
        
    }
}
