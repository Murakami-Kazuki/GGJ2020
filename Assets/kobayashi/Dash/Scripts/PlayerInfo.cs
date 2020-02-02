using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject); //シーン移動時に消されないように一旦避難
    }

    public void OnMovedResultScene()
    {
        GetComponent<playerControl>().enabled = false; //操作出来ないように。
        GetComponent<Rigidbody>().isKinematic = true;
        transform.rotation = Quaternion.Euler(0, 180, 0); //正面を向く
        transform.localScale = Vector3.one * 0.5f; //サイズの調整
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene()); //現在のシーンに移動
    }

    //リザルト画面状態に移動
    public void PrepareResultState()
    {
        transform.position = new Vector3(0, -1.03f, 3.14f);
    }
}
