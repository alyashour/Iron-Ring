using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class ParticleBehaviour : MonoBehaviour
{
    private BoulderBehaviour boulder;
    private int particleIndex;
    [SerializeField] Sprite[] sprites;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private Vector3 randDir;
    private float randSpeed;
    private float randomRotSpeed;

    private void Start()
    {
        boulder = transform.parent.gameObject.GetComponent<BoulderBehaviour>();
        particleIndex = boulder.index;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprites[particleIndex];
        transform.SetParent(null);

        randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        randSpeed = Random.Range(0.1f, 1f);
        randomRotSpeed = Random.Range(0.1f, 90f);
        float randSize = Random.Range(0.01f, 0.2f);
        transform.localScale = new Vector3(randSize, randSize, 1);

        Destroy(gameObject, 0.75f);
    }

    private void Update()
    {
        _rb.rotation += Time.deltaTime * randomRotSpeed;
        transform.position += randDir * Time.deltaTime * randSpeed;
    }
}