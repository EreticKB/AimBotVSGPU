using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    public UnityEngine.UI.Slider Volume;

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
        SaveHandler.LoadProperty(Game.IndexGameVolume, out float value, 0.5f);
        return value;
    }
}
