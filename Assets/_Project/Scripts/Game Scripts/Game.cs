using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    enum GameState
    {
        FirstLoad, //������ �������� ����.
        Playng, //������. :)
        StartWaiting, //������������� ������ ����� ��������������� ������� ������ ����� �������� �����.
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
