using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHairManager : MonoBehaviour
{
    List<GameObject> hairPartsList = new List<GameObject>();
    [SerializeField] Transform hairOyaTrans;
    public bool IsHage
    {
        get { return hairPartsList.Count == 0; }
    }

    void Awake()
    {
        //PrepareHair();
    }


    public GameObject[] AttackAndGetHairObjects(int damage) //髪を取られる
    {
        GameObject[] _hairObject = new GameObject[damage];
        for (int i = 0; i < damage; i++)
        {
            if (hairPartsList.Count <= 0)
                break;
            int randomID = Random.RandomRange(0, hairPartsList.Count);
            _hairObject[i] = hairPartsList[randomID];
            hairPartsList.RemoveAt(randomID);
        }
        hairPartsList.Clear();
        return _hairObject;
    }

    public void AddHaire(GameObject[] hair) //髪を奪う
    {
        for (int i = 0; i < hair.Length; i++)
        {
            if (hair[i] == null)
                continue;
            hairPartsList.Add(hair[i]);
            hair[i].transform.parent = transform;
            hair[i].transform.localPosition = Vector3.up * 0.8f;
            hair[i].transform.Rotate(Vector3.up * (Random.value - 0.5f) * 360f, Space.World);
            hair[i].transform.Rotate(Vector3.right * (Random.value - 0.5f) * 50f, Space.Self);
        }
    }

    public void PrepareHair()
    {
        var hairAmount = Random.Range(1, 5);

        for (int n = 0; n < hairAmount; n++)
        {
            var hairPrefab = HairManager.Instance.GetHairPrefab();
            var hair = Instantiate(hairPrefab);
            hair.transform.SetParent(hairOyaTrans);
            hair.transform.localPosition = Vector3.zero;
            hair.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hairPartsList.Add(hair);
            hair.GetComponent<BaseHair>().Activate();
            //Debug.Log("added hair:" + hair.name);
        }

    }


}
