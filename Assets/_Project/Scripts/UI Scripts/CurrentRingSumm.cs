using UnityEngine;
using UnityEngine.UI;

public class CurrentRingSumm : MonoBehaviour
{
    [SerializeField] Text _text;

    private void Update()
    {
        _text.text = $"Current: {RingPassed.EndlessRecord}";
    }
}
