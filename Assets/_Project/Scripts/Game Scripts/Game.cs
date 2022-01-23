using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] CanvasController CanvasRoot;
    public static Quaternion TiltOffSet = new Quaternion();
    public static Quaternion SavedTiltOffSet //� ����, ��� ����� ������ �������������� ����� ���������� � ������������� ����������, �� ���� ������ ���� ��������� �����.
    {
        get
        {
            Quaternion quaternion = new Quaternion();
            quaternion.x = PlayerPrefs.GetFloat("TiltQuaternionX", Quaternion.Euler(0, 0, 0).x);
            quaternion.y = PlayerPrefs.GetFloat("TiltQuaternionY", Quaternion.Euler(0, 0, 0).y);
            quaternion.z = PlayerPrefs.GetFloat("TiltQuaternionZ", Quaternion.Euler(0, 0, 0).z);
            quaternion.w = PlayerPrefs.GetFloat("TiltQuaternionW", Quaternion.Euler(0, 0, 0).w);
            return quaternion;
        }
        set
        {
            PlayerPrefs.SetFloat("TiltQuaternionX", value.x);
            PlayerPrefs.SetFloat("TiltQuaternionY", value.y);
            PlayerPrefs.SetFloat("TiltQuaternionZ", value.z);
            PlayerPrefs.SetFloat("TiltQuaternionW", value.w);
            PlayerPrefs.Save();
        }
    }

    

    enum GameState
    {
        FirstLoad, //������ �������� ����.
        PlayngEndless, //������. :)
        StartWaiting, //������������� ������ ����� ��������������� ������� ������ ����� �������� �����.
        Death,
        PlayingStory
    }
    static GameState _currentState = GameState.FirstLoad;
    public static void StartEndlessGame()
    {
        _currentState = GameState.PlayngEndless;
    }
    public static bool GameType; //true - ����������� �����, false - �������� �����.

    private void Awake()
    {
        TiltOffSet = SavedTiltOffSet;
        //if (_currentState != GameState.StartWaiting) _currentState = GameState.FirstLoad;
    }
    private void Start()
    {
        CanvasRoot.ActivateInterfaceByIndex(_currentState == GameState.FirstLoad ? 0 : 3);
    }
    void Update()
    {

    }

    public void SetDeath()
    {
        _currentState = GameState.Death;
    }
}
