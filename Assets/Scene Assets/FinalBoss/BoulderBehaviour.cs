using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBehaviour : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rb;

    private Vector3 moveDirection;

    private void Start()
    {
        player = GameObject.Find("Player");

        float speedValue = Random.Range(1f, 5f);
        Vector3 randomOffset = new Vector3(Random.Range(0, 0.25f), Random.Range(0, 0.25f), 0);


        moveDirection = player.transform.position - transform.position;
        rb.velocity = (moveDirection + randomOffset) * speedValue;

    }

}
