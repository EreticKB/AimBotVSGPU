using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject RingPrefab;
    private int _poolSize = 8;
    //private List<GameObject> _ring;
    private List<RingCollectionHandler> _ringCollectionScript;
    private Loop _loop;

    private void Awake()
    {
        _loop = new Loop(_poolSize-1,_poolSize,0);
        //_ring = new List<GameObject>(_poolSize);
        _ringCollectionScript = new List<RingCollectionHandler>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            _ringCollectionScript.Add(Instantiate(RingPrefab, new Vector3(0,0,0), Quaternion.Euler(0,0,0), transform).GetComponent<RingCollectionHandler>());
            _ringCollectionScript[i].LevelScript = this;
        }
    }
    private void Start()
    {
        _ringCollectionScript[0].EnableStartRing(Vector3.zero);
        for (int i = 1; i < _poolSize; i++)
        {
            //_ring[i].SetActive(true);
            _ringCollectionScript[i].EnableRing(0, 2, new Vector3(0, 0, 200 * i));
        }
    }

    public void MoveRingToNextPosition()
    {
        _ringCollectionScript[_loop.Next()].EnableRing(0, 2, new Vector3(0, 0, 200 * _poolSize));
    }
}
