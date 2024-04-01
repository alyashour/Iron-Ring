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
        float health = 2 + _bossBehaviour.enemyHealth * (12f/startHealth);
        float healthScaled = Mathf.Clamp(health, 2, 14);
        // Health scaled should be between 2 and 14
        _rt.sizeDelta = new Vector2(healthScaled, 3);
    }
}
