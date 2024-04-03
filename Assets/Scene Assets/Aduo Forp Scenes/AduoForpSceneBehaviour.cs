using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AduoForpSceneBehaviour : MonoBehaviour
{

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        // Check if the collided GameObject is the player or the dialogue trigger
        if (otherCollider.gameObject == player)
        {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }
}
