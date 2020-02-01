using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandom : MonoBehaviour
{
    private float angle;



    float x;
    float z;


    public bool enemymove;

    void OnEnable()
    {
        
        StartCoroutine(EnemyFloat());
        enemymove = true;
        SetAngle();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (enemymove == false)
        {
            StopCoroutine(EnemyFloat());
        }
        if(enemymove == false) Debug.Log(enemymove);
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

    
    
}
