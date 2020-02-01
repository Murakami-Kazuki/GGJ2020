using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapChip_houseWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //一つだけAcitve
        transform.Find("model/houseSET/set" + Random.RandomRange(0, transform.Find("model/houseSET").childCount)).gameObject.SetActive(true);
        transform.Find("model").Rotate(Vector3.up * 90f * Random.RandomRange(0, 99), Space.World);
    }

}
