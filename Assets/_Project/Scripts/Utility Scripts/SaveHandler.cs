using UnityEngine;

static class SaveHandler
{
    //============================================================
    public static void SaveProperty(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }
    public static void SaveProperty(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
    }
    public static void SaveProperty(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }
    //============================================================
    public static void LoadProperty(string name, out int value)
    {
        value = PlayerPrefs.GetInt(name, 0);
    }
    public static void LoadProperty(string name, out float value)
    {
        value = PlayerPrefs.GetFloat(name, 0);
    }
    public static void LoadProperty(string name, out string value)
    {
        value = PlayerPrefs.GetString(name, "");
    }
    //============================================================

}
