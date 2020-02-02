using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : SingletonMonoBehaviour<PlayerEffect>
{
    [SerializeField] GameObject [] playerEffects = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        ResetEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetEffect()
    {
        for (int i = 0; i < playerEffects.Length; i++)
        {
            playerEffects[i].SetActive(false);
        }
    }


    public void ShowPlayerEffect(int AttackLevel)
    {
        ResetEffect();
        if (AttackLevel == 1)
        {
            playerEffects[0].SetActive(true);
        }
        if (AttackLevel == 2)
        {
            playerEffects[1].SetActive(true);
        }
        if (AttackLevel == 3)
        {
            playerEffects[2].SetActive(true);
        }
    }
}
