using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HairResult : MonoBehaviour
{

    Dictionary<HairType, int> hairDict;
    //各髪のポイントを取得
    public Dictionary<HairType, int> MyHairDict
    {
        get
        {
            if (hairDict == null)
            {
                CalcHairResult();
            }
            return hairDict;
        }
    }

    /// <summary>
    //最終評価を取得
    //事前アナウンスによって評価対象が変わる
    /// </summary>
    public string GetResult(AnnounceType announceType)
    {
        if (announceType == AnnounceType.GachiGachi) return gachiResults[GetRank(hairDict[HairType.GachiGachi])];
        if (announceType == AnnounceType.MojaMoja) return gachiResults[GetRank(hairDict[HairType.MojaMoja])];
        if (announceType == AnnounceType.SaraSara) return gachiResults[GetRank(hairDict[HairType.SaraSara])];

        var minCount = hairDict.Min(n => n.Value);
        var maxCount = hairDict.Max(n => n.Value);
        var rank = GetRank(minCount, maxCount);
        return hadeResults[rank];
    }


    void CalcHairResult()
    {
        hairDict = new Dictionary<HairType, int>();
        for (int n = 0; n < 3; n++)
        {
            var type = (HairType)n;
            hairDict.Add(type, HairCountData.Load(type));
        }
    }

    List<string> hadeResults = new List<string>() {
        "イカさない",
        "イカしてる！",
        "とてもイカしてる！",
        "最っイカす！"
    };
    List<string> saraResults = new List<string>() {
        "麗しくない",
        "麗しい！",
        "とても麗しい！",
        "最っ麗しい！"
    };
    List<string> gachiResults = new List<string>() {
        "猛々しくない",
        "猛々しい！",
        "とても猛々しい！",
        "最っ猛々しい！"
    };
    List<string> mojaResults = new List<string>() {
        "いかめしくない",
        "いかめしい！",
        "とてもいかめしい！",
        "最っいかめしい！"
    };

    int GetRank(int num)
    {
        return GetRank(num, num);
    }

    int GetRank(int min, int max)
    {
        if (min <= 50 && max <= 50) return 0;
        if (50 < min && max <= 80) return 1;
        if (80 < min && max <= 99) return 2;

        return 3;
    }
}
