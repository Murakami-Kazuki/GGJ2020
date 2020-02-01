using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject player;
    Vector3 cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, cameraPos.y, playerPos.z+cameraPos.z);
    }
}
