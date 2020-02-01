using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour
{

    private const float mapChipSize = 5f;


    private int[] mapCode;


    // Start is called before the first frame update
    void Start()
    {
        //マップコードを読み込む
        LoadMapMaster();
        //ステージ生成実行
        CreateStage();
    }


    /// <summary>
    /// MapMaster
    /// マップコードを読み込む
    /// </summary>
    private void LoadMapMaster()
    {
        mapCode = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
    }


    /// <summary>
    /// マップチップAssetを取得
    /// </summary>
    /// <returns>The map chip asset name from map chip identifier.</returns>
    /// <param name="mapChipID">Map chip identifier.</param>
    private GameObject GetMapChipAsset_FromMapChipID(int mapChipID)
    {
        string assetName = "null";
        //************************************************************
        if (mapChipID == 0) assetName = "mapChip_road";
        if (mapChipID == 1) assetName = "";
        if (mapChipID == 2) assetName = "";
        if (mapChipID == 3) assetName = "";
        if (mapChipID == 4) assetName = "";
        if (mapChipID == 5) assetName = "";
        if (mapChipID == 6) assetName = "";
        if (mapChipID == 7) assetName = "";
        if (mapChipID == 8) assetName = "";
        if (mapChipID == 9) assetName = "";
        if (mapChipID == 10) assetName = "";
        if (mapChipID == 11) assetName = "";
        if (mapChipID == 12) assetName = "";
        if (mapChipID == 13) assetName = "";
        if (mapChipID == 14) assetName = "";

        if (assetName == "null")
        {
            Debug.Log("ERROR : mapChipAssetが存在しないぞ");
            return null;
        }

        GameObject mapChipAsset = Resources.Load<GameObject>("MapChips/" + assetName);

        return mapChipAsset;
    }


    /// <summary>
    /// ステージ生成
    /// </summary>
    private void CreateStage()
    {
        int mapSize = (int)Mathf.Sqrt(mapCode.Length);
        Vector3 mapPositionZero = new Vector3(-1, 0, -1) * (mapSize - 1) * mapChipSize * 0.5f;
        for (int iz = 0; iz < mapSize; iz++)
        {
            for (int ix = 0; ix < mapSize; ix++)
            {
                GameObject mapChip = Instantiate(GetMapChipAsset_FromMapChipID(mapCode[ix + iz * mapSize])) as GameObject;
                mapChip.transform.position = mapPositionZero + 
                (Vector3.right * ix + Vector3.forward * iz) * mapChipSize;
                mapChip.transform.parent = this.transform;
            }
        }
    }


}
