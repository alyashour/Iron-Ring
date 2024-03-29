using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBehaviour : MonoBehaviour
{
    private GameObject _boss;
    private RectTransform _rt;
    private RingBossBehaviour _bossBehaviour;

    private void Start()
    {
        _boss = GameObject.Find("RingBoss");
        _bossBehaviour = _boss.GetComponent<RingBossBehaviour>();
        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        float health = _bossBehaviour.enemyHealth * (14f/1000f);
        print(health);
        float healthScaled = Mathf.Clamp(health, 0, 14);
        //print(healthScaled);
        _rt.sizeDelta = new Vector2(healthScaled, 3);
    }



}
