using UnityEngine;

static class SaveHandler
//Класс создан для того, чтобы спокойно взаимодействовать с PlayerPrefs не задумываясь над типом передаваемых данных, что позволяет даже менять
//типы сохраняемых данных без изменения в обрабатывающем коде.
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
    public static void LoadProperty(string name, out int value, int defaultValue)
    {
        value = PlayerPrefs.GetInt(name, defaultValue);
    }
    public static void LoadProperty(string name, out int value)
    {
        LoadProperty(name, out value, 0);
    }
    //
    public static void LoadProperty(string name, out float value, float defaultValue)
    {
        value = PlayerPrefs.GetFloat(name, defaultValue);
    }
    public static void LoadProperty(string name, out float value)
    {
        LoadProperty(name, out value, 0);
    }
    //
    public static void LoadProperty(string name, out string value, string defaultValue)
    {
        value = PlayerPrefs.GetString(name, defaultValue);
    }
    public static void LoadProperty(string name, out string value)
    {
        LoadProperty(name, out value, "");
    }
    //
    //Использование целых чисел для хранения булевых переменных.
    public static void LoadProperty(string name, out bool value, bool defaultValue)
    {
        int state = PlayerPrefs.GetInt(name, defaultValue ? 1 : 0);
        value = state == 1 ? true : false;
    }
    public static void LoadProperty(string name, out bool value)
    {
        LoadProperty(name, out value, false);
    }
    public static void SaveProperty(string name, bool value)
    {
        if (value) PlayerPrefs.SetInt(name, 1);
        else PlayerPrefs.SetInt(name, 0);
        PlayerPrefs.Save();
    }
    //============================================================
}
