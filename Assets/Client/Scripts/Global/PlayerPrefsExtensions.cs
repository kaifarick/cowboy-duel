using System;
using UnityEngine;

public class PlayerPrefsExtensions
{
    public static DateTime GetDate(string key, DateTime defaultTime = default)
    {
        if (PlayerPrefs.HasKey(key))
        {
            long dateTime = long.Parse(PlayerPrefs.GetString(key));
            return DateTime.FromBinary(dateTime);
        }

        SetDate(key, defaultTime);
        return defaultTime;
    }

    public static void SetDate(string key, DateTime dateTime)
    {
        PlayerPrefs.SetString(key, dateTime.ToBinary().ToString());
    }

    public static bool GetBool(string key, bool defaultBool = false)
    {
        if (PlayerPrefs.HasKey(key))
        {
            bool value = PlayerPrefs.GetInt(key) == 1;
            return value;
        }

        SetBool(key, defaultBool);
        return defaultBool;
    }

    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }
}
