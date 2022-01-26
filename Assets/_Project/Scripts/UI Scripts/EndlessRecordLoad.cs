using UnityEngine;
using UnityEngine.UI;

public class EndlessRecordLoad : MonoBehaviour
{
    [SerializeField] private Text _text;
    void Start()
    {
        SaveHandler.LoadProperty(Game.IndexEndlessRecord, out int record, 0);
        _text.text = $"Record: {record}";
    }
}
