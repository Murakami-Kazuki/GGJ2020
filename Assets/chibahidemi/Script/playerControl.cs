using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{

    private const int PARAM_maxSTR = 3;
    private const int PARAM_minSTR = 1;



    private float AngleSpeed = 90f;//回転スピード
    private float moveX = 0.0f;
    private float moveZ = 0.0f;
    public Rigidbody rb;

    public bool isAttack=false;

    float targetAngle;
    Vector3 lookAtVec;
    float angleY;

    float hagage = 0.0f;
    float maxHagage = 300f;//ハゲージのmaxの値
    float AttackPower = 0.5f;//ダッシュの速さ
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        /*//playerの左スティック動作
        if (0.6f < Input.GetAxis("Horizontal") || Input.GetAxis("Horizontal") <= -0.6f)
        {
            moveX = Input.GetAxis("Horizontal");
        }
        else {
            moveX = 0;
        }

        if (0.6f < Input.GetAxis("Vertical") || Input.GetAxis("Vertical") <= -0.6f)
        {
            moveZ = Input.GetAxis("Vertical");
        }
        else {
            moveZ = 0;
        }

        Vector3 direction = new Vector3(moveX * speed, 0, -1 * (moveZ * speed)) * Time.deltaTime;

        //Debug.Log("yoko = " + Input.GetAxis("Horizontal") + " : tate = " + Input.GetAxis("Vertical"));
        */

        //左スティックで方向転換（のみ）
        if (0.6f < Input.GetAxis("Horizontal") || Input.GetAxis("Horizontal") <= -0.6f)
        {
            moveX = Input.GetAxis("Horizontal");
            lookAtVectol();
        }
        else {
            moveX = 0;
        }

        if (0.6f < Input.GetAxis("Vertical") || Input.GetAxis("Vertical") <= -0.6f)
        {
            moveZ = Input.GetAxis("Vertical");
            lookAtVectol();
        }
        else {
            moveZ = 0;
        }

        lookAtVec = new Vector3(moveX, 0, -moveZ);

        //ボタン押してゲージをためる
        if (Input.GetButton("DS4square"))
        {
            if (hagage < maxHagage)
            {
                hagage += 300f * Time.deltaTime;
            }
        }

        //ボタン離すとダッシュ
        if (Input.GetButtonUp("DS4square"))
        {
            rb.AddForce(transform.forward * hagage * AttackPower, ForceMode.Impulse);
            hagage = 0;

        }
        //Debug.Log(hagage);

        
        //ダッシュ後の挙動
        if (1f < rb.velocity.magnitude)
        {
            
            //rb.AddForce(transform.right * moveX*100);
            //rb.AddForce(transform.forward * AttackPower * 100f,ForceMode.Acceleration);
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }

    }

    void FixedUpdate()
    {
        //rb.velocity = new Vector3(moveX * speed, 0, -1 * (moveZ * speed)) * Time.deltaTime;
        //rb.AddForce(moveX * speed, 0, moveZ * speed);
    }

    float GetAim(Vector3 from, Vector3 to)
    {
        float dx = from.x - to.x;
        float dz = from.z - to.z;
        float rad = Mathf.Atan2(dx, dz);
        return rad * Mathf.Rad2Deg + 180f;
    }

    void lookAtVectol()
    {
        targetAngle = GetAim(transform.position, transform.position + lookAtVec);
        angleY = Mathf.MoveTowardsAngle
         (
          transform.eulerAngles.y,
          targetAngle,
          AngleSpeed * Time.deltaTime
         );
        transform.eulerAngles = Vector3.up * angleY;
    }


    public List<GameObject> hairObject;
    public void AddHair(GameObject[] _hairObject)
    {
        for (int i = 0; i < _hairObject.Length; i++)
        {
            if (_hairObject[i] == null)
                continue;
            hairObject.Add(_hairObject[i]);
            _hairObject[i].transform.parent = transform;
            _hairObject[i].transform.localPosition = Vector3.up * 1.50f;
            _hairObject[i].transform.Rotate(Vector3.up * (Random.value - 0.5f) * 360f, Space.World);
            _hairObject[i].transform.Rotate(Vector3.right * (Random.value - 0.5f) * 50f, Space.Self);
        }
       
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            if (isAttack)
            {
                AddHair(collider.gameObject.GetComponent<EnemyHairManager>().AttackAndGetHairObjects(9));
            }
            else
            {
                collider.gameObject.GetComponent<EnemyHairManager>().AddHaire(DamegeAndSendHairObjects(3));
            }

        }
    }

    public GameObject[] DamegeAndSendHairObjects(int damage)
    {
        GameObject[] SendObject = new GameObject[damage];
        for (int i = 0; i < damage; i++)
        {
            if (hairObject.Count <= 0)
                break;
            int randomID = Random.RandomRange(0, hairObject.Count);
            SendObject[i] = hairObject[randomID];
            hairObject.RemoveAt(randomID);
        }
        return SendObject;

    }
}