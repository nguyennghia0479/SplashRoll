using UnityEngine;

public static class SaveManager
{
    public static void SaveSFX(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public static float LoadSFX(string key)
    {
        float defaultValue = .5f;
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public static void SaveLocale(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static int LoadLocale(string key)
    {
        int defaultValue = 0;
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SaveLevel(string key, LevelSaveData levelData)
    {
        string saveValue = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString(key, saveValue);
    }

    public static LevelSaveData LoadLevel(string key)
    {
        string defaultValue = null;
        string loadValue = PlayerPrefs.GetString(key, defaultValue);

        if (string.IsNullOrEmpty(loadValue))
            return null;

        LevelSaveData obj = JsonUtility.FromJson<LevelSaveData>(loadValue);
        return obj;
    }
}
