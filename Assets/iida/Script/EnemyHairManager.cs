using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHairManager : MonoBehaviour
{
    public List<GameObject> hairObject = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public GameObject AttackAndGetHairObject()
    {
        int randomID = Random.RandomRange(0, hairObject.Count);
        GameObject _hairObject = null;
        _hairObject = hairObject[randomID];
        hairObject.Remove(hairObject[randomID]);
        for (int i = 0; i < hairObject.Count; i++)
        {
            hairObject[i].SetActive(false);
        }
        hairObject.Clear();
       
        return _hairObject;
            
    }

    public void AddHaire(GameObject[] hair)
    {
        for (int i = 0; i < hair.Length; i++)
        {
            hairObject.Add(hair[i]);
            hair[i].transform.parent = transform;
            hair[i].transform.localPosition = Vector3.up * 0.8f;
            hair[i].transform.Rotate(Vector3.up * (Random.value - 0.5f) * 360f, Space.World);
            hair[i].transform.Rotate(Vector3.right * (Random.value - 0.5f) * 50f, Space.Self);
        }
    }


}
