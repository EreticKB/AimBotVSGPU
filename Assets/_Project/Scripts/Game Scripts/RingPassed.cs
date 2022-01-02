using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPassed : MonoBehaviour
{
    private Ring _ring;

    private void Awake()
    {
        _ring = GetComponent<Ring>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _ring.DestroyMe();
    }
}
