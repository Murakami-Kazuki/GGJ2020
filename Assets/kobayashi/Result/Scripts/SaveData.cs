using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{

    #region hair
    public static int LoadHair(HairType type)
    {
        return PlayerPrefs.GetInt(type.ToString(), 0);
    }
    public static void SaveHair(HairType type, int count)
    {
        PlayerPrefs.SetInt(type.ToString(), count);
    }

    /// <summary>
    // 次のプレイに影響しないようにdataを元に戻す
    /// </summary>
    public static void ResetAllHairCount()
    {
        int max = 3;//HairType count
        for (int n = 0; n < max; n++) PlayerPrefs.SetInt(((HairType)n).ToString(), 0);
    }
    #endregion

    #region announce
    public static void SaveAnnounce(AnnounceType type)
    {
        PlayerPrefs.SetInt("AnnounceType", (int)type);
    }

    public static AnnounceType LoadAnnounce()
    {
        return (AnnounceType)PlayerPrefs.GetInt("Announce", 0);
    }

    #endregion
}

public enum HairType
{
    SaraSara,
    GachiGachi,
    MojaMoja,
}

public enum AnnounceType
{
    SaraSara,
    GachiGachi,
    MojaMoja,
    HadeHade,
}