using UnityEngine;

public class GlobalVolume : MonoBehaviour
{
    public UnityEngine.UI.Slider Volume;

    private void Awake()
    {
        AudioListener.volume = _loadVolume();
        Volume.value = _loadVolume()-1;
    }
    public void VolumeUpdate(float value)
    {
        AudioListener.volume = Volume.value+1f;
        SaveHandler.SaveProperty(Game.IndexGameVolume, Volume.value+1);

    }
    private static float _loadVolume()
    {
        SaveHandler.LoadProperty(Game.IndexGameVolume, out float value, 1.5f);
        return value;
    }
}
