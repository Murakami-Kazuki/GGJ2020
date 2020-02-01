using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandom : MonoBehaviour
{
    EnemyManager enemy;
    private CharacterController controller;
    private float speed;
    private float gravity = 20.0f;
    private float angle;
    private Vector3 moveDirection;


    float x;
    float z;


    bool enemymove;

    void OnEnable()
    {
        
        StartCoroutine(EnemyFloat());
        enemymove = true;
        enemy = GetComponent<EnemyManager>();
        speed = enemy.EnemySpeed;
        SetAngle();
        

    }

    // Update is called once per frame
    void Update()
    {

        EnemyMove();
        if (enemymove == false)
        {
            StopCoroutine(EnemyFloat());
        }
    }

    /*
     * 動きの流れ
     */
    private IEnumerator EnemyFloat()
    {
        float second;

        SetAngle();

        second = Random.Range(1, 5);


        yield return new WaitForSeconds(second);

        StartCoroutine(EnemyFloat());
    }


    /*
     *敵の向きをランダムで決める
     */
    private void SetAngle()
    {
        
        float max = 360;
        float min = 0;

        if(enemymove)angle = Random.Range(min, max); 
    }
    /*
    * 敵の向き
    */
    void EnemyAngle()
    {
        var currentAngle = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(currentAngle, angle, Time.deltaTime), 0);
    }
    /*
     * 反対に向く
     */
    void OppositeRotate()
    {
        var currentAngle = transform.eulerAngles.y;
        angle = (currentAngle + 180);
    }

    /*
    *敵の動き
    */
    private void EnemyMove()
    {

        controller = GetComponent<CharacterController>();
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(transform.forward * Time.deltaTime * speed);
        RayTarget();
        EnemyAngle();
        //Debug.Log("x成分" + x + "z成分" + z);

    }





    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        float distance = 2f;
        Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Wall")))
        {
            OppositeRotate();
            enemymove = false;
            Debug.Log(enemymove);
        }
        else
        {
            enemymove = true;
            Debug.Log(enemymove);

        }
    }

    
}
