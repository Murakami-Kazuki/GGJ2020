using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : SingletonMonoBehaviour<playerControl>
{

    private const int PARAM_maxSTR = 3;
    private const int PARAM_minSTR = 1;


    //各種パラメーター
    //カチカチ
    private const float PARAM_chargeSpeed_min = 200f;
    private const float PARAM_chargeSpeed_max = 1000f;
    private const float PARAM_chargeSpeed_getHairParamUp = 20f;
    //サラサラ
    private const float PARAM_attackPower_min = 0.5f;
    private const float PARAM_attackPower_max = 1.0f;
    private const float PARAM_attackPower_getHairParamUp = 0.12f;
    //もじゃもじゃ
    private const float PARAM_rotateSpeed_min = 90f;
    private const float PARAM_rotateSpeed_max = 360f;
    private const float PARAM_rotateSpeed_getHairParamUp = 30f;

    private float param_chargeSpeed = PARAM_chargeSpeed_min;//charge速度
    private float param_rotateSpeed = PARAM_rotateSpeed_min;//回転スピー
    private float param_attackPower = PARAM_attackPower_min;//ダッシュの速さ
    private enum GetHairType
    {
        kachikachi,
        sarasara,
        mojamoja,
    }

    /// <summary>
    /// ステータス変化
    /// </summary>
    /// <param name="getHairType">ヘアータイプ</param>
    /// <param name="getLength">取得ヘアー数</param>
    private void GetHairPlusParam(GetHairType getHairType, int getLength = 1)
    {
        switch (getHairType)
        {
            case GetHairType.kachikachi:
                param_chargeSpeed += PARAM_chargeSpeed_getHairParamUp * (float)getLength;
                param_chargeSpeed = Mathf.Clamp(param_chargeSpeed, PARAM_chargeSpeed_min, PARAM_chargeSpeed_max);
                break;
            case GetHairType.sarasara:
                param_rotateSpeed += PARAM_rotateSpeed_getHairParamUp * (float)getLength;
                param_rotateSpeed = Mathf.Clamp(param_rotateSpeed, PARAM_rotateSpeed_min, PARAM_rotateSpeed_max);
                break;
            case GetHairType.mojamoja:
                param_attackPower += PARAM_attackPower_getHairParamUp * (float)getLength;
                param_attackPower = Mathf.Clamp(param_attackPower, PARAM_attackPower_min, PARAM_attackPower_max);
                break;
        }
    }


    private float moveX = 0.0f;
    private float moveZ = 0.0f;
    public Rigidbody rb;

    public bool isAttack = false;

    float targetAngle;
    Vector3 lookAtVec;
    float angleY;

    float hagage = 0.0f;
    float maxHagage = 300f;//ハゲージのmaxの値

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

        if (Input.GetKey(KeyCode.Z))
        {
            moveX = 1;
            lookAtVectol();
        }
        else if (Input.GetKey(KeyCode.X))
        {
            moveX = -1;
            lookAtVectol();
        }

        lookAtVec = new Vector3(moveX, 0, -moveZ);

        //ボタン押してゲージをためる
        if (Input.GetButton("DS4square") || Input.GetKey(KeyCode.A))
        {
            if (hagage < maxHagage)
            {
                hagage += 300f * Time.deltaTime;
                haGaugeUI.UpdatePowerCallback(Mathf.Clamp01(hagage / maxHagage)); //ゲージの表示
                StartCoroutine(playerVoice.Instance.ChargeVC());

                //プレイヤーエフェクトを出す
                attackLevel = haGaugeUI.GaugeLevel;
                PlayerEffect.Instance.ShowPlayerEffect(attackLevel);
            }
            else haGaugeUI.UpdatePowerCallback(1f);
        }

        //ボタン離すとダッシュ
        if (Input.GetButtonUp("DS4square") || Input.GetKeyUp(KeyCode.A))
        {
            //プレイヤーエフェクトを消す
            PlayerEffect.Instance.ResetEffect();

            StartCoroutine(playerVoice.Instance.ScreemVC());
            rb.AddForce(transform.forward * hagage * param_attackPower, ForceMode.Impulse);
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
            isAttack = true;
            aniCon.SetBool("isAttack", true);
        }
        else
        {
            isAttack = false;
            aniCon.SetBool("isAttack", false);
            attackLevel = 0;
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
          param_rotateSpeed * Time.deltaTime
         );
        transform.eulerAngles = Vector3.up * angleY;
    }

    [SerializeField] Transform oyaTrans;
    public List<GameObject> hairObject;
    public void AddHair(GameObject[] _hairObject)
    {
        for (int i = 0; i < _hairObject.Length; i++)
        {
            if (_hairObject[i] == null)
                continue;
            hairObject.Add(_hairObject[i]);
            _hairObject[i].transform.parent = oyaTrans;
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
            collision.gameObject.GetComponent<EnemyHairManager>().AddHaire(DamegeAndSendHairObjects(3));
            StartCoroutine(playerVoice.Instance.DamagteVC());
            return;
        }

        Debug.Log("collide. attackLevel:" + attackLevel);

        collision.gameObject.GetComponent<EnemyManager>().Hit(attackLevel);

        if (attackLevel <= 1) return;
        if (attackLevel == 2) AddHair(collision.gameObject.GetComponent<EnemyHairManager>().AttackAndGetHairObjects(1));
        if (attackLevel == 3) AddHair(collision.gameObject.GetComponent<EnemyHairManager>().AttackAndGetHairObjects(10)); //10ではなく全部
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