using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HairCountData
{
    public static int Load(HairType type)
    {
        return PlayerPrefs.GetInt(type.ToString(), 0);
    }
    public static void Save(HairType type, int count)
    {
        PlayerPrefs.SetInt(type.ToString(), count);
    }

    /// <summary>
    // 次のプレイに影響しないようにdataを元に戻す
    /// </summary>
    public static void ResetAll()
    {
        int max = 3;//HairType count
        for (int n = 0; n < max; n++) PlayerPrefs.SetInt(((HairType)n).ToString(), 0);
    }

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