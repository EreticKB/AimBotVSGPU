using UnityEngine;
public class MenuScriptControllerRoot : MonoBehaviour
{
    //родительский ко всем меню контроллерам.
    public bool IsChangeNeeded;
    public int NextMenuIndex;
    private void OnEnable()
    {
        IsChangeNeeded = false;
    }
    protected void MenuTravel(int index)
    {
        IsChangeNeeded = true;
        NextMenuIndex = index;
    }
}