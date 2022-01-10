using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    enum GameState
    {
        FirstLoad, //Первая загрузка игры.
        Playng, //Играем. :)
        StartWaiting, //трехсекундный таймер перед непосредственно началом полета после загрузки сцены.
        Death
    }
    static GameState _currentState;

    private void Awake()
    {
        if (_currentState != GameState.StartWaiting) _currentState = GameState.FirstLoad;
    }
    void Update()
    {
        
    }

    public void SetDeath()
    {
        _currentState = GameState.Death;
    }
}
