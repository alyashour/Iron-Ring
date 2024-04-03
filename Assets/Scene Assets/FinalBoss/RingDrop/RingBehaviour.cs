using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehaviour : MonoBehaviour
{
    [SerializeField] float amplitude;

    private SceneInitialization _sceneInitialization;
    private void Start()
    {
        _sceneInitialization = GameObject.Find("SceneController").GetComponent<SceneInitialization>();
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * amplitude, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            _sceneInitialization.sceneState = 10;
            Destroy(gameObject, 0.5f);
        }
        



    }
}