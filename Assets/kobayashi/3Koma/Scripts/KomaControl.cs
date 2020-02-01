using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaControl : MonoBehaviour
{

    [SerializeField] List<BaseKoma> komaList;

    [SerializeField] Title.PressKeyText pressKeyText;

    void Start()
    {
        StartCoroutine(ShowKomaList());
    }

    IEnumerator ShowKomaList()
    {
        for (int n = 0; n < komaList.Count; n++)
        {
            yield return komaList[n].Show();
        }

        yield return null;
        pressKeyText.Show();
    }
}
