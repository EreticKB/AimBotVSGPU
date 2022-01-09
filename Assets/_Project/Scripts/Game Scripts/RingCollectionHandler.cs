using System.Collections.Generic;
using UnityEngine;

public class RingCollectionHandler : MonoBehaviour
{
    public List<GameObject> Rings;
    private List<Ring> _ringScripts;
    public Transform Transform;
    public Level LevelScript;
    private void Awake()
    {
        _ringScripts = new List<Ring>(Rings.Count);
        Transform = transform;
        for (int i = 0; i < Rings.Count; i++) _ringScripts.Add(Rings[i].GetComponent<Ring>());
    }

    public void EnableRing(int maxIndex, int difficulty, Vector3 position)
    {
        Transform.position += position;
        Transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        maxIndex = maxIndex > 0 ? maxIndex : 0;
        maxIndex = maxIndex < Rings.Count ? maxIndex : Rings.Count - 1;
        int ringIndex = Random.Range(0, maxIndex + 1);
        Rings[ringIndex].SetActive(true);
        _ringScripts[ringIndex].SetUpRing(difficulty);
    }
    public void DisableRings()
    {
        for (int i = 0; i < Rings.Count; i++) Rings[i].SetActive(false);
        RingWasPassed();
    }
    public void RingWasPassed()
    {
        LevelScript.MoveRingToNextPosition();
    }
}
