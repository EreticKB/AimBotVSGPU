using UnityEngine;


public class Game : MonoBehaviour
{
    public static readonly string IndexGameVolume = "GameVolume";
    public static readonly string IndexControlSensitivity = "ControlSensitivity";
    public static readonly string IndexTiltActivation = "TiltActivation";
    [SerializeField] CanvasController CanvasRoot;
    public static Quaternion TiltOffSet = new Quaternion();
    public static Quaternion SavedTiltOffSet //я знаю, что можно просто конвертировать юнити кватернион в сериализуемый кватернион, но буду делать если останется время.
    {
        get
        {
            Quaternion quaternion = new Quaternion();
            //SaveHandler.LoadProperty("TiltQuaternionX", out quaternion.x);


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
        FirstLoad, //Первая загрузка игры.
        PlayngEndless, //Играем. :)
        StartWaiting, //трехсекундный таймер перед непосредственно началом полета после загрузки сцены.
        Death,
        PlayingStory
    }
    static GameState _currentState = GameState.FirstLoad;
    public static void StartEndlessGame()
    {
        _currentState = GameState.PlayngEndless;
    }
    public static bool GameType; //true - бесконечный режим, false - сюжетный режим.

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
