using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHair : MonoBehaviour
{
    [SerializeField] public HairType myType;

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }
}
