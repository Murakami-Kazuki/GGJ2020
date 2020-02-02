using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    Vector3 basePos;
    [SerializeField] Transform targetTrans;
    [SerializeField] int speed = 5;

    void Awake()
    {
        basePos = transform.position - targetTrans.position;
    }

    void Update()
    {
        SyncPos();
    }

    void SyncPos()
    {
        var idealPos = targetTrans.position;
        idealPos += basePos;
        var nextPos = Vector3.Lerp(transform.position, idealPos, Time.deltaTime * speed);
        transform.position = nextPos;
    }
}