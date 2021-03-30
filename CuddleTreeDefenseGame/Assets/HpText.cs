using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HpText : MonoBehaviour
{
    TextMeshProUGUI hpTextMeshPro;

    private void Awake()
    {
        hpTextMeshPro = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        hpTextMeshPro.text = transform.parent.parent.GetComponent<EnemyDragon>().Hp.ToString();
    }
}
