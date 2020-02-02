using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HairManager : SingletonMonoBehaviour<HairManager>
{
    [SerializeField] Transform gachiList;
    [SerializeField] Transform mojaList;
    [SerializeField] Transform saraList;



    List<GameObject> gachigachiHairPrefabs;
    List<GameObject> mojamojaHairPrefabs;
    List<GameObject> sarasaraHairPrefabs;

    protected override void Awake()
    {
        base.Awake();
        mojamojaHairPrefabs = mojaList.GetComponentsInChildren<BaseHair>(true).Select(n => n.gameObject).ToList();
        gachigachiHairPrefabs = gachiList.GetComponentsInChildren<BaseHair>(true).Select(n => n.gameObject).ToList();
        sarasaraHairPrefabs = saraList.GetComponentsInChildren<BaseHair>(true).Select(n => n.gameObject).ToList();
    }

    public GameObject GetHairPrefab()
    {
        var rnd = Random.Range(0, 3);
        if (rnd == 0) return gachigachiHairPrefabs[Random.Range(0, gachigachiHairPrefabs.Count)];
        if (rnd == 1) return mojamojaHairPrefabs[Random.Range(0, mojamojaHairPrefabs.Count)];

        return sarasaraHairPrefabs[Random.Range(0, sarasaraHairPrefabs.Count)];
    }
}
