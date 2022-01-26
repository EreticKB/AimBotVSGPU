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
            for (int i = _poolSize / 2; i < _poolSize; i++) _ringCollectionScript[i].EnableRing(2, 1, new Vector3(0, 0, 400 * i));
        }
    }

    public void MoveRingToNextPosition()
    {
        if (LevelType == LevelTypeList.Endless)
        {
            if (RingPassed.EndlessRecord < 3) _ringCollectionScript[_loop.Next()].EnableRing(3, 1, new Vector3(0, 0, 400 * _poolSize));
            else if (RingPassed.EndlessRecord < 8) _ringCollectionScript[_loop.Next()].EnableRing(3, 2, new Vector3(0, 0, 400 * _poolSize));
            else if (RingPassed.EndlessRecord < 13) _ringCollectionScript[_loop.Next()].EnableRing(4, 2, new Vector3(0, 0, 400 * _poolSize));
            else if (RingPassed.EndlessRecord < 18) _ringCollectionScript[_loop.Next()].EnableRing(5, 2, new Vector3(0, 0, 400 * _poolSize));
            else if (RingPassed.EndlessRecord < 23) _ringCollectionScript[_loop.Next()].EnableRing(6, 2, new Vector3(0, 0, 400 * _poolSize));
            else if (RingPassed.EndlessRecord < 28) _ringCollectionScript[_loop.Next()].EnableRing(6, 3, new Vector3(0, 0, 400 * _poolSize));
            else if (RingPassed.EndlessRecord < 50) _ringCollectionScript[_loop.Next()].EnableRing(6, 4, new Vector3(0, 0, 400 * _poolSize));
            else _ringCollectionScript[_loop.Next()].EnableRing(6, 5, new Vector3(0, 0, 400 * _poolSize));
            
        }
    }
}
