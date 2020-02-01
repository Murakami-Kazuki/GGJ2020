using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveEscape : MonoBehaviour
{

    EnemyManager enemy;
    Vector3 player_pos;
    public float TargetRange;
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
    private void OnEnable()
    {
        enemy = GetComponent<EnemyManager>();
    }

    bool NearPlayer()
    {
        player_pos = playerControl.Instance.gameObject.transform.position;
        float distance = (transform.position - player_pos).sqrMagnitude;
        if (distance < TargetRange)
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

       
        if (is_player)
        {

            var random = GetComponent<EnemyMoveRandom>();
            random.enabled = false;
            enemy.OppositeRotate();

            StartCoroutine(Speedup());

            
        }
        else
        {
            var random = GetComponent<EnemyMoveRandom>();
            random.enabled = true;
            
  
        }
            
        //Debug.Log(NearPlayer() + "" + is_player);
    }
    IEnumerator Speedup()
    {
        enemy.EnemySpeed = enemy.InitializeSpeed * 2;

        yield return new WaitForSeconds(0.5f);

        enemy.EnemySpeed--;
        enemy.EnemySpeed = enemy.InitializeSpeed;


    }

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        float distance = 5f;
        Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.SphereCast(ray, 2,out hit, distance, LayerMask.GetMask("Player")))
        {

            is_player = true;
        }
        else
        {
            is_player = false;
        }

    }
}
