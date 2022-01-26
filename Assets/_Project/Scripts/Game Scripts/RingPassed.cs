using UnityEngine;

public class RingPassed : MonoBehaviour
{
    private RingCollectionHandler _ring;
    public static int EndlessRecord
    {
        get;
        private set;
    }
    private void Awake()
    {
        EndlessRecord = 0;
        _ring = GetComponent<RingCollectionHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        EndlessRecord++;
        _ring.DisableRings();
    }
}
