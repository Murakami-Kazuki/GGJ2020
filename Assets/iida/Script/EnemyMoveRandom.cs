using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRandom : MonoBehaviour
{
    private float angle;


    void OnEnable()
    {
        
        StartCoroutine(EnemyFloat());
        SetAngle();
        

    }

    // Update is called once per frame
    void Update()
    {

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

        angle = Random.Range(min, max); 
    }

    
    
}
