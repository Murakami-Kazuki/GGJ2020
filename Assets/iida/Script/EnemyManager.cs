
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("敵速度")] public float EnemySpeed;
    [SerializeField] GameObject[] DamageEffect = new GameObject[2];
    [HideInInspector] public float InitializeSpeed;
    [HideInInspector] public float angle;
    private CharacterController controller;
    private Vector3 moveDirection;
    private float gravity = 20.0f;

    GameObject effect;
    Rigidbody rb;
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
        CheckEnemyHair();
    }
    private void OnEnable()
    {
        InitializeSpeed = EnemySpeed;
        enemyHair = GetComponent<EnemyHairManager>();
        enemyHair = GetComponent<EnemyHairManager>();
        rb = GetComponent<Rigidbody>();
        angle = Random.Range(0, 360);


    }

    void CheckEnemyHair()
    {

        if (enemyHair.IsHage)
        {
            //Debug.Log("ハゲ");
            bald = true;
        }
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
        angle = currentAngle + (180 * Random.Range(-1, 1));
    }


    /*
    *敵の動き
    */
    private void EnemyMove()
    {

        rb.AddForce(transform.forward * EnemySpeed, ForceMode.Acceleration);
        RayTarget();
        EnemyAngle(angle);

    }

    void RayTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        float distance = 2f;

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Wall")))
        {
            OppositeRotate();
            rb.velocity = Vector3.zero;

        }
    }

    public void Hit(int hagageLevel)
    {
        Debug.Log("Hoge");
        Debug.Log(hagageLevel);

        if (hagageLevel == 2)
        {
            effect = Instantiate(DamageEffect[0]);
            effect.transform.position = transform.position;

        }
        if (hagageLevel == 3)
        {
            effect = Instantiate(DamageEffect[1]);
            effect.transform.position = transform.position;

        }
        StartCoroutine(setfalseEffect(hagageLevel, effect));


    }
    IEnumerator setfalseEffect(int hagageLevel, GameObject effect)

    {


        yield return new WaitForSeconds(1);

        Destroy(effect);

    }
    void Knockback(Collision collision)
    {
        var c_rb = collision.transform.gameObject.GetComponent<Rigidbody>();
        c_rb.AddForce(transform.forward * 5, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

            Knockback(collision);


        }
    }
}
