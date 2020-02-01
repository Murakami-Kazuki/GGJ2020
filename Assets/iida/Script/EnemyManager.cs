using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("敵速度")] public float EnemySpeed;
    [HideInInspector] public float InitializeSpeed;
    [HideInInspector]public float angle;
    private CharacterController controller;
    private Vector3 moveDirection;
    private float gravity = 20.0f;


    EnemyHairManager enemyHair;
    public bool bald;

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
        InitializeSpeed = EnemySpeed;
        enemyHair = GetComponent<EnemyHairManager>();

    }
    /*
    * 敵の向き
    */
    public void EnemyAngle(float angle)
    {
        var currentAngle = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(currentAngle, angle, Time.deltaTime * 2), 0);
    }
    /*
     * 反対に向く
     */
    public void OppositeRotate()
    {
        var currentAngle = transform.eulerAngles.y;
        angle = currentAngle + (180 * Random.Range(-1,1));
    }


    /*
    *敵の動き
    */
    private void EnemyMove()
    {
        controller = GetComponent<CharacterController>();
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= EnemySpeed;
        moveDirection.y -= gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            moveDirection = transform.forward * EnemySpeed;
        }
        else 
        {
            moveDirection.y -= gravity * Time.deltaTime; 
        }

        controller.Move(transform.forward * Time.deltaTime * EnemySpeed);
        RayTarget();
        EnemyAngle(angle);

    }

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        float distance = 2f;
        //Debug.DrawLine(ray.origin, ray.direction * distance + ray.origin, Color.red);

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Wall")))
        {
            OppositeRotate();

        }
    }

    
    void Hit()
    {
        if (bald)
        {
            TakeHair();
        }
        else
        {
            GetHair();
        }
    }
    void TakeHair()
    {
       
    }
    void GetHair()
    {

    }
     
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("hit");
            Hit();

        }     
    }


}
        