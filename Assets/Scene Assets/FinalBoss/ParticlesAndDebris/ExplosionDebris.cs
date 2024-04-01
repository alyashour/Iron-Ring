using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class ExplosionDebris : MonoBehaviour
{
    private void Start()
    {
        float time = Random.Range(0.1f, 3f);
        Destroy(gameObject, time);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime;
    }
}