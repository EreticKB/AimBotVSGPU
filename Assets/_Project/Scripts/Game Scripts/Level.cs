using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject RingPrefab;
    private int _poolSize = 6;
    private List<GameObject> _ring;
    private List<RingCollectionHandler> _ringScript;


    private void Awake()
    {
        _ring = new List<GameObject>(_poolSize);
        _ringScript = new List<RingCollectionHandler>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            _ring.Add(Instantiate(RingPrefab, transform));
            _ringScript.Add(_ring[i].GetComponent<RingCollectionHandler>());
            _ring[i].SetActive(false);
        }
    }
    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            _ringScript[i].Transform.position = new Vector3(0,0,300*(i+1));
            _ringScript[i].Transform.rotation = Quaternion.Euler(0,0,Random.Range(0f,360f));
            _ring[i].SetActive(true);
            _ringScript[i].EnableRing(3, 2);
        }
    }

    private void Update()
    {
        
    }
}
