using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author - Aiden

public class BoxBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    public void OnHit(Vector3 playerPos)
    {
        Vector3 direction = playerPos - transform.position;
        rb.velocity = -direction.normalized * 10f;
    }

}