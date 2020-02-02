using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//リザルト画面をマネージする
public class ResultManager : MonoBehaviour
{
    [SerializeField] Transform player;
    void Start()
    {
        var info = GameObject.FindObjectOfType<PlayerInfo>();
        if (info != null)
        {
            //playerをリザルト画面用にして、仮置きされたplayerと交換する
            info.OnMovedResultScene();
            info.PrepareResultState();
            player.transform.gameObject.SetActive(false);
            player = info.transform;
        }
    }
}
