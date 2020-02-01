﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveChase : MonoBehaviour
{
    //Player player;//プレヤーができたら
    [SerializeField] GameObject player;
    private Vector3 moveDirection;
    private CharacterController controller;
    Vector3 player_pos;
    private float gravity = 20;
    float speed;
    public float TargetRange;

    bool is_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
       
        is_player = false;
        speed = GetComponent<EnemyManager>().EnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool NearPlayer()
    {
        player_pos = player.transform.position;
        float distance = (transform.position - player_pos).sqrMagnitude;
        if (distance < TargetRange * TargetRange)
        {
            return true;
        }

        return false;
    }
    /*
 *敵の動き
 */
    private void EnemyMove()
    {
        FindPlayer();
        controller = GetComponent<CharacterController>();
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(transform.forward * Time.deltaTime * speed);
        RayTarget();

    }


    void FindPlayer()
    {
        if (NearPlayer() && is_player)
        {
            transform.LookAt(player_pos);
            EnemyMove();
            GetComponent<EnemyMoveRandom>().enabled = false;
            Debug.Log("敵を追いかける");
        }else{
            GetComponent<EnemyMoveRandom>().enabled = true;
        }

    }

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        RaycastHit hit;
        float distance = 2f;
        Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Player")))
        {
            Debug.Log("目の前にプレイヤーがいる" + hit.collider.name);
            is_player = true;
        }
        
    }
}
