using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollectionHandler : MonoBehaviour
{
    public List<GameObject> Rings;
    private List<Ring> _ringScripts;
    private Transform _transform;
    public Level LevelScript;
    private Vector3 _scale;
    [SerializeField]private Collider _trigger;
    [SerializeField]private float _sizeUpTime = 3f;
    private void Awake()
    {
        _ringScripts = new List<Ring>(Rings.Count);
        _transform = transform;
        _transform.position = Vector3.zero;
        for (int i = 0; i < Rings.Count; i++) _ringScripts.Add(Rings[i].GetComponent<Ring>());
    }

    
    public void EnableRing(int maxIndex, int difficulty, Vector3 distance, bool isInflate, bool isPlaying) //функция передвигает кольцо с текущей позиции на указанное расстояние.
    {
        _trigger.enabled = isPlaying;
        _scale = new Vector3(0.1f, 0.1f, 1f);
        _transform.localScale = _scale;
        _transform.position += distance;
        _transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        maxIndex = maxIndex > 0 ? maxIndex : 0;
        maxIndex = maxIndex < Rings.Count ? maxIndex : Rings.Count - 1;
        int ringIndex = Random.Range(1, maxIndex + 1);
        Rings[ringIndex].SetActive(true);
        _ringScripts[ringIndex].SetUpRing(difficulty);
        if (isInflate) StartCoroutine(SizeUp(0));
        else StartCoroutine(SizeUp(0.99f));
    }

    public void ResetRingPosition(Vector3 zero)
    {
        _transform.position = zero;
        DisableRings();
    }
    public void EnableStartRing(Vector3 position)
    {
        _transform.position = position;
        Rings[0].SetActive(true);
    }
    public void DisableRings()
    {
        for (int i = 0; i < Rings.Count; i++) Rings[i].SetActive(false);
    }
    public void RingWasPassed()
    {
        DisableRings();
        LevelScript.MoveRingToNextPosition();
    }

    IEnumerator SizeUp(float scale)
    {
        while (scale < 1)
        {
        scale += Time.deltaTime / _sizeUpTime;
        if (scale > 1) scale = 1;
        _transform.localScale = new Vector3(Mathf.Lerp(_scale.x, 1f, scale), Mathf.Lerp(_scale.y, 1f, scale), 1);
        yield return null;
        }
    }

}
