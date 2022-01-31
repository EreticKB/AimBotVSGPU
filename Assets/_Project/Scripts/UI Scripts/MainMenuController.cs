
using UnityEngine.SceneManagement;

public class MainMenuController : MenuScriptControllerRoot
{   
    public void GoToLevelSelect()
    {
        MenuTravel(2);
    }

    public void GoToSettings()
    {
        MenuTravel(1);
    }

    public void StarEndlessMode()
    {
        Game.StartEndlessGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
