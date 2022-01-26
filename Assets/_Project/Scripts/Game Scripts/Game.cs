using UnityEngine;
//ќбщий класс, хран€щий наиболее глобальные параметры и управл€ющий основным игровым процессом.
public class Game : MonoBehaviour
{
    public static readonly string IndexGameVolume = "GameVolume";
    public static readonly string IndexControlSensitivity = "ControlSensitivity";
    public static readonly string IndexTiltActivation = "TiltActivation";
    
    public float StartTimer;

    [SerializeField] FlyInterfaceController _flyInterface;
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] CanvasController CanvasRoot;
    public static Quaternion TiltOffSet = new Quaternion();
    public static Quaternion SavedTiltOffSet //€ знаю, что можно просто конвертировать юнити кватернион в сериализуемый кватернион, но буду делать если останетс€ врем€.
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
        }
    }

    enum GameState
    {
        FirstLoad, //ѕерва€ загрузка игры.
        PlayngEndless, //»граем. :)
        StartWaiting, //трехсекундный таймер перед непосредственно началом полета после загрузки сцены.
        Death,
        PlayingStory
    }
    static GameState _currentState = GameState.FirstLoad;
    
    

    public static void StartEndlessGame()
    {
        _currentState = GameState.PlayngEndless;
    }

    private void Awake()
    {
        TiltOffSet = SavedTiltOffSet;
    }
    private void Start()
    {
        CanvasRoot.ActivateInterfaceByIndex(_currentState == GameState.FirstLoad ? 0 : 3);
        if (_currentState == GameState.PlayngEndless) _flyInterface.SetEndlessRecordActive();
        if (_currentState == GameState.PlayingStory) _flyInterface.SetLevelProgressActive();
        _flyInterface.SetStartTimer(StartTimer);

    }
    public static void SetDeath()
    {
        _currentState = GameState.Death;
    }
}
