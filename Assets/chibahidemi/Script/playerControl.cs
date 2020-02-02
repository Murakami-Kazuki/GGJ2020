using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : SingletonMonoBehaviour<playerControl>
{

    private const int PARAM_maxSTR = 3;
    private const int PARAM_minSTR = 1;



    private float AngleSpeed = 90f;//回転スピード
    private float moveX = 0.0f;
    private float moveZ = 0.0f;
    public Rigidbody rb;

    public bool isAttack = false;

    float targetAngle;
    Vector3 lookAtVec;
    float angleY;

    float hagage = 0.0f;
    float maxHagage = 300f;//ハゲージのmaxの値
    float AttackPower = 0.5f;//ダッシュの速さ
    [SerializeField] Dash.HaGauge haGaugeUI; //ハゲージのUI
    [SerializeField] PlayerCanvas playerCanvas;
    int attackLevel; //アタック時のレベル
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aniCon = GetComponentInChildren<Animator>();
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
        else
        {
            moveX = 0;
        }

        if (0.6f < Input.GetAxis("Vertical") || Input.GetAxis("Vertical") <= -0.6f)
        {
            moveZ = Input.GetAxis("Vertical");
            lookAtVectol();
        }
        else
        {
            moveZ = 0;
        }

        lookAtVec = new Vector3(moveX, 0, -moveZ);

        //ボタン押してゲージをためる
        if (Input.GetButton("DS4square") || Input.GetKey(KeyCode.A))
        {
            if (hagage < maxHagage)
            {
                hagage += 300f * Time.deltaTime;
                haGaugeUI.UpdatePowerCallback(Mathf.Clamp01(hagage / maxHagage)); //ゲージの表示
            }
            else haGaugeUI.UpdatePowerCallback(1f);
        }

        //ボタン離すとダッシュ
        if (Input.GetButtonUp("DS4square") || Input.GetKeyUp(KeyCode.A))
        {
            rb.AddForce(transform.forward * hagage * AttackPower, ForceMode.Impulse);
            if (0.0f <= hagage && hagage < 100f) PlayAnimation(AnimationType.attack0);
            if (100.0f <= hagage && hagage < 200f) PlayAnimation(AnimationType.attack1);
            if (200.0f <= hagage && hagage < 900f) PlayAnimation(AnimationType.attack2);
            hagage = 0;
            attackLevel = haGaugeUI.GaugeLevel;
            haGaugeUI.UpdatePowerCallback(0f); //ゲージをリセット
        }
        //Debug.Log(hagage);


        //ダッシュ後の挙動
        if (1f < rb.velocity.magnitude)
        {

            //rb.AddForce(transform.right * moveX*100);
            //rb.AddForce(transform.forward * AttackPower * 100f,ForceMode.Acceleration);
            isAttack = true;
            aniCon.SetBool("isAttack", true);
        }
        else
        {
            isAttack = false;
            aniCon.SetBool("isAttack", false);
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
            playerCanvas.GetHair(_hairObject[i].GetComponent<BaseHair>().myType);
        }

    }


    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log("hoge");
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("OnCollison!!!");
    //        if (isAttack)
    //        {
    //            AddHair(collision.gameObject.GetComponent<EnemyHairManager>().AttackAndGetHairObject());
    //        }
    //        else
    //        {
    //            collision.gameObject.GetComponent<EnemyHairManager>().AddHaire(DamegeAndSendHairObject(3));
    //        }

    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        if (!isAttack)
        {
            GetComponent<Collider>().gameObject.GetComponent<EnemyHairManager>().AddHaire(DamegeAndSendHairObjects(3));
            return;
        }

        if (attackLevel == 0) return;
        if (attackLevel == 1) AddHair(collision.gameObject.GetComponent<EnemyHairManager>().AttackAndGetHairObjects(1));
        if (attackLevel == 2) AddHair(collision.gameObject.GetComponent<EnemyHairManager>().AttackAndGetHairObjects(10)); //10ではなく全部
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



    private Animator aniCon;
    private enum AnimationType
    {
        stay,
        charge,
        attack0,
        attack1,
        attack2,
    }
    /// <summary>
    /// アニメーション再生
    /// </summary>
    private void PlayAnimation(AnimationType animationType)
    {
        if (aniCon == null) return;

        switch (animationType)
        {
            case AnimationType.stay:
                aniCon.Play("chara_stay");
                break;
            case AnimationType.charge:
                aniCon.Play("chara_charge");
                break;
            case AnimationType.attack0:
                aniCon.Play("chara_attack0");
                break;
            case AnimationType.attack1:
                aniCon.Play("chara_attack1");
                break;
            case AnimationType.attack2:
                aniCon.Play("chara_attack2");
                break;
        }
    }




    public float getHagage()
    {
        return hagage;
    }
}