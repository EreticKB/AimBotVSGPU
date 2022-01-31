using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject RingPrefab;
    private int _poolSize = 8;
    private List<RingCollectionHandler> _ringCollectionScript;
    private Loop _loop;
    private int _difficulty;
    private int _ringTypes;
    private bool _isSizeUpNeeded = true;
    private bool _isPlaying = true;
    public enum LevelTypeList
    {
        None,
        Endless,
        Story
    }
    public static LevelTypeList LevelType = LevelTypeList.None;

    private void Awake()
    {
        if (LevelType == LevelTypeList.None) return;
        _loop = new Loop(_poolSize - 1, _poolSize, 0);
        _ringCollectionScript = new List<RingCollectionHandler>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            _ringCollectionScript.Add(Instantiate(RingPrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), transform).GetComponent<RingCollectionHandler>());
            _ringCollectionScript[i].LevelScript = this;
        }
    }
    private void Start()
    {
        if (LevelType == LevelTypeList.None) return;
        PlaceRings(0);
    }

    private void PlaceRings(float zeroPoint)
    {
        _ringCollectionScript[0].EnableStartRing(new Vector3(0,0, zeroPoint));
        for (int i = 0; i < _poolSize; i++) _ringCollectionScript[i].ResetRingPosition(new Vector3(0, 0, zeroPoint));
        if (LevelType == LevelTypeList.Endless)
        {
            SetGeneration(2, 1);
            _isSizeUpNeeded = true;
            _isPlaying = true;
        }
        else if (LevelType == LevelTypeList.None)
        {
            _isSizeUpNeeded = false;
            _isPlaying = false;
        }
        for (int i = 1; i < _poolSize / 2; i++)
        {
            _ringCollectionScript[i].EnableRing(0, 0, new Vector3(0, 0, (400 * i)), _isSizeUpNeeded, _isPlaying);
        }
        for (int i = _poolSize / 2; i < _poolSize; i++) _ringCollectionScript[i].EnableRing(_ringTypes, _difficulty, new Vector3(0, 0, (400 * i)), _isSizeUpNeeded, _isPlaying);
    }

    public void MoveRingToNextPosition()
    {
        if (LevelType == LevelTypeList.Endless)
        {
            if (RingPassed.EndlessRecord < 3) SetGeneration(3, 5);
            else if (RingPassed.EndlessRecord < 18) SetGeneration(4, 5);
            else if (RingPassed.EndlessRecord < 30) SetGeneration(5, 5);
            else if (RingPassed.EndlessRecord < 40) SetGeneration(6, 10);
            else if (RingPassed.EndlessRecord < 60) SetGeneration(7, 10);
            else if (RingPassed.EndlessRecord < 80) SetGeneration(7, 10);
            else if (RingPassed.EndlessRecord < 100) SetGeneration(9, 15);
            else if (RingPassed.EndlessRecord < 110) SetGeneration(9, 15);
            else if (RingPassed.EndlessRecord < 130) SetGeneration(10, 15);
            else SetGeneration(10, 20);

        }
        if (LevelType != LevelTypeList.None) _ringCollectionScript[_loop.Next()].EnableRing(_ringTypes, _difficulty, new Vector3(0, 0, 400 * _poolSize), _isSizeUpNeeded, _isPlaying);
    }
    private void SetGeneration(int ringTypes, int difficulty)
    {
        _ringTypes = ringTypes;
        _difficulty = difficulty;
    }

    public void SetEnd()
    {
        LevelType = LevelTypeList.None;
        SetGeneration(0, 20);
        PlaceRings(_ringCollectionScript[_loop.Next()].transform.position.z);
    }
}
