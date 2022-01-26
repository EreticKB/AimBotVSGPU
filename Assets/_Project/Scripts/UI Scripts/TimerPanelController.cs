using UnityEngine;
using UnityEngine.UI;

public class TimerPanelController : MonoBehaviour
{

    public float Timer;
    private float _timer;
    [SerializeField] Text _counter;
    private void OnEnable()
    {
        _timer = Timer;
    }
    // Update is called once per frame
    void Update()
    {
        _counter.text = Timer.ToString("F0");
        Timer -= Time.deltaTime;
        if (Timer <= 0) gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Timer = _timer;
    }
}
