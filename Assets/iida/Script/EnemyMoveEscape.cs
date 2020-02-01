using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveEscape : MonoBehaviour
{
    //Player player;//プレヤーができたら
    [SerializeField] GameObject player;
    private Vector3 moveDirection;
    private CharacterController controller;
    Vector3 player_pos;
    private float gravity = 20;
    float speed;
    public float TargetRange;
    float angle;
    bool is_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
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
        RayTarget();

    }


    void FindPlayer()
    {
        if (NearPlayer() || is_player)
        {
            var currentAngle = transform.eulerAngles.y;
            angle = (currentAngle + 180);
            
            Debug.Log("離れる" + -player.transform.forward + "角度" + currentAngle + 180);
        }
        //Debug.Log(NearPlayer() + "" + is_player);
    }

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        float distance = 20f;
        Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Player")))
        {
            //Debug.Log("目の前にプレイヤーがいる" + hit.collider.name);
            is_player = true;
        }
        else
        {
            is_player = false;
        }

    }
}
