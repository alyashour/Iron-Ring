using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private void Update()
    {
        rb.rotation += Time.deltaTime * 90;
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 5) * 0.0001f, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerAttributes.PlayerXP += 1;
            Destroy(gameObject);
        }
        
    }
}