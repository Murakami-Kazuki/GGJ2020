using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSpawner : SingletonMonoBehaviour<CharaSpawner>
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform playerTrans;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    void Start()
    {
        StartCoroutine(Spawning());
    }
    IEnumerator Spawning()
    {
        yield return null;
        SpawnFirst();
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return null;
        }
    }

    void SpawnFirst()
    {
        var spawnAmount = 50;
        //Debug.Log("okk");

        for (int n = 0; n < spawnAmount; n++)
        {
            Vector3 spawnPos;
            var canSpawn = GetSpawnPoint(out spawnPos);
            if (!canSpawn) continue;
            //Debug.Log("okk:" + spawnPos);
            spawnPos.y = 2.16f;
            var prefab = GetEnemyPrefab();
            var enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<EnemyHairManager>().PrepareHair(false);
        }
    }

    public void SpawnHage()
    {
        var spawnAmount = 20;
        //Debug.Log("okk");

        for (int n = 0; n < spawnAmount; n++)
        {
            Vector3 spawnPos;
            var canSpawn = GetSpawnPoint(out spawnPos);
            if (!canSpawn) continue;
            //Debug.Log("okk:" + spawnPos);
            spawnPos.y = 2.16f;
            var prefab = GetEnemyPrefab();
            var enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<EnemyHairManager>().PrepareHair(true);
        }
    }

    bool GetSpawnPoint(out Vector3 spawnPoint)
    {
        int tryCount = 0;
        spawnPoint = Vector3.zero;

        while (tryCount < 10)
        {
            var pos = new Vector3(Random.Range(-50, 50), 2, Random.Range(-50, 50));
            var ray = new Ray(pos, Vector3.down);
            Debug.DrawRay(ray.origin, Vector3.down * 10, Color.red, 10);
            RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 10)) Debug.Log("ok:" + hit.transform.name + " layer:" + hit.transform.CompareTag("Road"));
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (!hit.transform.CompareTag("Road"))
                {
                    tryCount++;
                    continue;
                }

                if ((playerTrans.position - hit.point).magnitude < 10)
                {
                    tryCount++;
                    continue;
                }

                spawnPoint = hit.point;
                break;
            }


            spawnPoint = hit.point;
            tryCount++;
        }


        if (10 <= tryCount) return false;
        return true;
    }
}
