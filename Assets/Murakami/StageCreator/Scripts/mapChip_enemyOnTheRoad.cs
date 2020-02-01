using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapChip_enemyOnTheRoad : MonoBehaviour
{
    [SerializeField]
    private GameObject asset_enemy;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = Instantiate(asset_enemy);
        enemy.transform.position = transform.position + Vector3.up * 2.0f;
    }



}
