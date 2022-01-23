using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    public UnityEngine.UI.Slider Volume;

    private float _gameVolume
    {
        get => PlayerPrefs.GetFloat(Game.IndexGameVolume, 0.5f);
        set => PlayerPrefs.SetFloat(Game.IndexGameVolume, value);
    }

    private void Awake()
    {
        AudioListener.volume = _loadVolume();
        Volume.value = _loadVolume();
    }
    public void VolumeUpdate(float value)
    {
        AudioListener.volume = Volume.value;
        SaveHandler.SaveProperty(Game.IndexGameVolume, Volume.value);

    }
    private static float _loadVolume()
    {
        float value;
        SaveHandler.LoadProperty(Game.IndexGameVolume, out value);
        return value;
    }
}
