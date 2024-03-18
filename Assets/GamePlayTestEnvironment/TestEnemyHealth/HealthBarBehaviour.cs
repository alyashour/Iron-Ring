using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author - Aiden

public class HealthBarBehaviour : MonoBehaviour
{

    [SerializeField] RockBehaviour parentRock;

    private void Start()
    {
        parentRock = transform.parent.parent.GetComponent<RockBehaviour>();
    }

    private void Update()
    {
        float x = Mathf.Clamp(1, 0, 1);
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}