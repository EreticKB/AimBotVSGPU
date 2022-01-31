using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SerializedStructContainer;
//����� �����, �������� �������� ���������� ��������� � ����������� �������� ������� ���������.
public class Game : MonoBehaviour
{
    public static readonly string IndexGameVolume = "GameVolume";
    public static readonly string IndexControlSensitivity = "ControlSensitivity";
    public static readonly string IndexTiltActivation = "TiltActivation";
    public static readonly string IndexEndlessRecord = "EndlessRecord";
    public static readonly string IndexTiltOffSet = "TiltOffSet";
    public float StartTimer;
    [SerializeField] FlyInterfaceController _flyInterface;
    [SerializeField] PlayerBehaviour _player;
    [SerializeField] Level _level;
    [SerializeField] CanvasController _canvasRoot;
    [SerializeField] AudioSource _mainMenuMusic;
    public static Quaternion TiltOffSet = new Quaternion();
    public static Quaternion SavedTiltOffSet
    {
        get
        {
            SaveHandler.LoadProperty(IndexTiltOffSet, out SerializableQuaternion quaternion, Quaternion.Euler(0, 0, 0));
            return quaternion;
        }
        set
        {
            SaveHandler.SaveProperty(IndexTiltOffSet, value);
        }
    }
    enum GameState
    {
        FirstLoad, //������ �������� ����.
        PlayngEndless, //������. :)
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
        _player.GetComponent<PlayerBehaviour>().ResetPosition(900);
        _flyInterface.SetCrashMenuActive(false);
        RingPassed.EndlessRecord = RingPassed.EndlessRecord<10 ? 0: RingPassed.EndlessRecord - 10;
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
