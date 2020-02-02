using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveChase : MonoBehaviour
{
    Vector3 player_pos;
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
        EnemyMove();
    }
    bool NearPlayer()
    {
        player_pos = playerControl.Instance.gameObject.transform.position;
        float distance = (transform.position - player_pos).sqrMagnitude;
        if (distance < TargetRange && distance > 2f)
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
            transform.LookAt(new Vector3(player_pos.x, transform.position.y, player_pos.z));
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<EnemyManager>().EnemySpeed, ForceMode.Impulse);
            Debug.Log("敵を追いかける");
        }
        //Debug.Log(NearPlayer()+ ""+  is_player);
    }

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        float distance = 10f;
        Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.SphereCast(ray, 4, out hit, distance, LayerMask.GetMask("Player")))
        {
            Debug.Log("目の前にプレイヤーがいる" + hit.collider.name);
            is_player = true;
        }
        else
        {
            is_player = false;
        }

    }
}
