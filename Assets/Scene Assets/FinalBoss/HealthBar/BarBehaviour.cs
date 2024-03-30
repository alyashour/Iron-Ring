using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class BarBehaviour : MonoBehaviour
{
    private GameObject _boss;
    private RectTransform _rt;
    private RingBossBehaviour _bossBehaviour;

    float startHealth;

    private void Start()
    {
        _boss = GameObject.Find("RingBoss");
        _bossBehaviour = _boss.GetComponent<RingBossBehaviour>();
        _rt = GetComponent<RectTransform>();
        startHealth = _bossBehaviour.enemyHealth;
    }

    private void Update()
    {
        float health = _bossBehaviour.enemyHealth * (14f/startHealth);
        float healthScaled = Mathf.Clamp(health, 0, 14);
        _rt.sizeDelta = new Vector2(healthScaled, 3);
    }
}
