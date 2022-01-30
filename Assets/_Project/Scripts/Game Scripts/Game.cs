using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//ќбщий класс, хран€щий наиболее глобальные параметры и управл€ющий основным игровым процессом.
public class Game : MonoBehaviour
{
    public static readonly string IndexGameVolume = "GameVolume";
    public static readonly string IndexControlSensitivity = "ControlSensitivity";
    public static readonly string IndexTiltActivation = "TiltActivation";
    public static readonly string IndexEndlessRecord = "EndlessRecord";
    public float StartTimer;

    [SerializeField] FlyInterfaceController _flyInterface;
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] Level _level;
    [SerializeField] CanvasController _canvasRoot;
    [SerializeField] AudioSource _mainMenuMusic;
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
        PlayingStory,
        DeathEndless,
        DeathStory
    }
    static GameState _currentState = GameState.FirstLoad;
    private void Awake()
    {
        AdsHandler.SetGame(this);
        TiltOffSet = SavedTiltOffSet;
        if (_currentState == GameState.FirstLoad) _mainMenuMusic.Play();
        else _mainMenuMusic.Stop();
    }
    private void Start()
    {
        _canvasRoot.ActivateInterfaceByIndex(_currentState == GameState.FirstLoad ? 0 : 3);
        if (_currentState == GameState.FirstLoad) return;
        if (_currentState == GameState.PlayngEndless)
        {
            _flyInterface.SetEndlessRecordActive();
        }
        if (_currentState == GameState.PlayingStory)
        {
            _flyInterface.SetLevelProgressActive();
        }
        _flyInterface.SetStartTimer(StartTimer);
        StartCoroutine(startCountDown(StartTimer));
    }

    private void Update()
    {
        if (_player.CurrentPlayerState == PlayerBehaviour.PlayerState.Crush) if (_player.DisEngageEngine())_flyInterface.SetCrashMenuActive(true);
        if (_currentState == GameState.DeathEndless)
        {
            _currentState = GameState.FirstLoad;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public static void StartEndlessGame()
    {
        _currentState = GameState.PlayngEndless;
        Level.LevelType = Level.LevelTypeList.Endless;

    }

    public static void StartStoryGame()
    {
        _currentState = GameState.PlayingStory;
        Level.LevelType = Level.LevelTypeList.Story;

    }

    public void SetEndAnimation()
    {
        _level.SetEnd();
        _player.SetEnd();
        SaveHandler.LoadProperty(IndexEndlessRecord, out int record, 0);
        if (record < RingPassed.EndlessRecord) SaveHandler.SaveProperty(IndexEndlessRecord, RingPassed.EndlessRecord);
    }
    public void SetDeath()
    {
        if (_currentState == GameState.PlayngEndless) _currentState = GameState.DeathEndless;
        else _currentState = GameState.DeathStory;
    }

    public void UseContinue()
    {
        _player.GetComponent<PlayerBehaviour>().ResetPosition(600);
        _flyInterface.SetCrashMenuActive(false);
        Start();
    }
    IEnumerator startCountDown(float timer)
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        _player.EngadeEngine();
    }
}
