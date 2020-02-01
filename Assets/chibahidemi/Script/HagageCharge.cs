using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HagageCharge : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().fillAmount =
        player.GetComponent<playerControl>().getHagage()/300;
        //100f / 300f;
        //200f/300f;
        
    }
}
