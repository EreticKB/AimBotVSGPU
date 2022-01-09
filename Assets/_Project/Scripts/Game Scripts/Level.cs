using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject RingPrefab;
    private int _poolSize = 10;
    //private List<GameObject> _ring;
    private List<RingCollectionHandler> _ringScript;
    private Loop _loop;

    private void Awake()
    {
        _loop = new Loop(_poolSize-1,_poolSize,0);
        //_ring = new List<GameObject>(_poolSize);
        _ringScript = new List<RingCollectionHandler>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            _ringScript.Add(Instantiate(RingPrefab, new Vector3(0,0,0), Quaternion.Euler(0,0,0), transform).GetComponent<RingCollectionHandler>());
            _ringScript[i].LevelScript = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            //_ring[i].SetActive(true);
            _ringScript[i].EnableRing(0, 2, new Vector3(0, 0, 200 * (i + 1)));
        }
    }

    public void MoveRingToNextPosition()
    {
        _ringScript[_loop.Next()].EnableRing(0, 2, new Vector3(0, 0, 200 * _poolSize));
    }
}
