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
        _ringCollectionScript[0].EnableStartRing(Vector3.zero);
        for (int i = 1; i < _poolSize / 2; i++)
        {
            //_ring[i].SetActive(true);
            _ringCollectionScript[i].EnableRing(0, 0, new Vector3(0, 0, 400 * i));
        }
        if (LevelType == LevelTypeList.Endless)
        {
            SetGeneration(2, 1);
            for (int i = _poolSize / 2; i < _poolSize; i++) _ringCollectionScript[i].EnableRing(_ringTypes, _difficulty, new Vector3(0, 0, 400 * i));
        }
    }

    public void MoveRingToNextPosition()
    {
        if (LevelType == LevelTypeList.Endless)
        {
            if (RingPassed.EndlessRecord < 3) SetGeneration(3,1);
            else if (RingPassed.EndlessRecord < 18) SetGeneration(4,2);
            else if (RingPassed.EndlessRecord < 30) SetGeneration(5,3);
            else if (RingPassed.EndlessRecord < 40) SetGeneration(6,3);
            else if (RingPassed.EndlessRecord < 60) SetGeneration(7, 3);
            else if (RingPassed.EndlessRecord < 80) SetGeneration(7, 4);
            else if (RingPassed.EndlessRecord < 100) SetGeneration(9,4);
            else if (RingPassed.EndlessRecord < 110) SetGeneration(9, 5);
            else if (RingPassed.EndlessRecord < 130) SetGeneration(10, 5);
            else SetGeneration(10, 6);

        }
        _ringCollectionScript[_loop.Next()].EnableRing(_ringTypes, _difficulty, new Vector3(0, 0, 400 * _poolSize));
    }
    private void SetGeneration(int ringTypes, int difficulty)
    {
        _ringTypes = ringTypes;
        _difficulty = difficulty;
    }
}
