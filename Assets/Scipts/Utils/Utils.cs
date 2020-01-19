using UnityEngine;

public static class Utility
{
    public const string SaveKey = "Save";

    public static void SerializeData(object toSave, string key)
    {
        string save = JsonUtility.ToJson(toSave);        
        PlayerPrefs.SetString(key, save);
    }
    
    public static T DeserializeData<T>(string key)
    {
        string save = PlayerPrefs.GetString(key);
        T saveObject = JsonUtility.FromJson<T>(save);       
        return saveObject;
    }

    public static void DeleteSaveKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public static bool IsSaveEmpty(string key)
    {
        return string.IsNullOrEmpty(PlayerPrefs.GetString(key));
    }

}

