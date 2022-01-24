using UnityEngine;
//����� �����, �������� �������� ���������� ��������� � ����������� �������� ������� ���������.
public class Game : MonoBehaviour
{
    public static readonly string IndexGameVolume = "GameVolume";
    public static readonly string IndexControlSensitivity = "ControlSensitivity";
    public static readonly string IndexTiltActivation = "TiltActivation";

    [SerializeField] CanvasController CanvasRoot;
    public static Quaternion TiltOffSet = new Quaternion();
    public static Quaternion SavedTiltOffSet //� ����, ��� ����� ������ �������������� ����� ���������� � ������������� ����������, �� ���� ������ ���� ��������� �����.
    {
        get
        {
            Quaternion quaternion = new Quaternion();
            SaveHandler.LoadProperty("TiltQuaternionX", out quaternion.x, Quaternion.Euler(0, 0, 0).x);
            SaveHandler.LoadProperty("TiltQuaternionY", out quaternion.y, Quaternion.Euler(0, 0, 0).y);
            SaveHandler.LoadProperty("TiltQuaternionZ", out quaternion.z, Quaternion.Euler(0, 0, 0).z);
            SaveHandler.LoadProperty("TiltQuaternionW", out quaternion.w, Quaternion.Euler(0, 0, 0).w);
            return quaternion;
        }
        set
        {
            SaveHandler.SaveProperty("TiltQuaternionX", value.x);
            SaveHandler.SaveProperty("TiltQuaternionY", value.y);
            SaveHandler.SaveProperty("TiltQuaternionZ", value.z);
            SaveHandler.SaveProperty("TiltQuaternionW", value.w);
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
