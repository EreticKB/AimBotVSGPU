using System.Collections.Generic;
using UnityEngine;

public class RingCollectionHandler : MonoBehaviour
{
    public List<GameObject> Rings;
    public Transform Transform;

    private void Awake()
    {
        Transform = transform;
    }

    public void EnableRing(int maxIndex, int difficulty)
    {
        maxIndex = maxIndex > 0 ? maxIndex : 0;
        maxIndex = maxIndex < Rings.Count ? maxIndex : Rings.Count - 1;
        int ringIndex = Random.Range(0, maxIndex + 1);
        Rings[ringIndex].SetActive(true);
        Rings[ringIndex].GetComponent<Ring>().SetUpRing(difficulty);
    }
    public void DisableRings()
    {
        for (int i = 0; i < Rings.Count; i++) Rings[i].SetActive(false);
    }
}
