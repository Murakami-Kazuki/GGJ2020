using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h;
        float v;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h * Time.deltaTime * 10, 0, v*Time.deltaTime * 10);
    }
}
