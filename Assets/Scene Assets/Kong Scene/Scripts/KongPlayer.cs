using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongPlayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] runSprites;
    private int spriteIndex;

    private new Rigidbody2D rigidbody;
    private Vector2 direction;

    public float moveSpeed = 1f;
    public float jumpStrength = 4f;

    private new Collider2D collider;

    private bool grounded;
    private bool climbing;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                if (contactPoint.normal.y > 0.5)
                {
                    grounded = true;
                    break;
                } else
                {
                    Physics2D.IgnoreCollision(collision.collider, collider, true); 
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            climbing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            climbing = false;
            foreach (var platform in FindObjectsOfType<Collider2D>())
            {
                if (platform.CompareTag("Platform"))
                {
                    Physics2D.IgnoreCollision(collider, platform, false);
                }
            }
        }
    }

    private void Update()
    {

        if (grounded && Input.GetButtonDown("Jump")){
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;
        if (grounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        if (direction.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (direction.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }



        if (climbing)
        {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }

    private void AnimateSprite()
    {
        if (direction.x != 0f && grounded)
        {
            spriteIndex++;
            if (spriteIndex >= runSprites.Length)
            {
                spriteIndex = 0;
            }
            spriteRenderer.sprite = runSprites[spriteIndex];
        }
        
    }
}
