using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandom : MonoBehaviour
{
    EnemyManager enemy;
    Rigidbody rg;
    private CharacterController controller;
    private float speed;
    public float gravity = 20.0F;
    private float angle;
    private Vector3 moveDirection;


    float x;
    float z;

    bool enemymove = true;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();

    }

    void OnEnable()
    {
        moveDirection = Vector3.zero;
        StartCoroutine(EnemyFloat());
        speed = GetComponent<EnemyManager>().EnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {

        EnemyMove();
        RayTarget();

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

        second = Random.Range(0.5f, 5);


        yield return new WaitForSeconds(second);

        StartCoroutine(EnemyFloat());
    }


    /*
     * 敵の向く方向をランダムで決定
     */
    private void SetAngle()
    {
            float max = 1;
            float min = -1;
            x = Random.Range(min, max);
            z = Random.Range(min, max);


           
        
    }

    /*
     * 敵の向き
     */
     private void EnemyRotate(float x,float z)
    {
        var currentAngle = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(currentAngle, Angle(x, z) * Mathf.Rad2Deg, 0.1f), 0);
    }
    float Angle(float x, float z)
    {
        float theta;

        theta = Mathf.Atan2(x, z);

        return theta;
    }

    /*
    *敵の動き
    */
    private void EnemyMove()
    {

        controller = GetComponent<CharacterController>();
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(transform.forward * Time.deltaTime * speed);
        EnemyRotate(x, z);
        //Debug.Log("x成分" + x + "z成分" + z);

    }

    

    

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        float distance = 5f;
        Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Wall")))
        {
            //if (hit.collider.CompareTag("Wall"))
            //{
                Debug.Log("目の前に壁がある" + hit.collider.name);
            transform.eulerAngles += new Vector3(0, Mathf.LerpAngle(transform.eulerAngles.y, transform.eulerAngles.y+180, 0.1f), 0);
            //SetAngle();
            StopCoroutine(EnemyFloat());
                enemymove = false;
                Debug.Log(enemymove);
            //}
        }
        else
        {
            enemymove = true;
            StartCoroutine(EnemyFloat());
            Debug.Log(enemymove);

        }
    }
}
