using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPassed : MonoBehaviour
{
    private RingCollectionHandler _ring;

    private void Awake()
    {
        _ring = GetComponent<RingCollectionHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _ring.DisableRings();
    }
}
