using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongColliderManager : MonoBehaviour
{
    public BoxCollider2D kongCollider;

    private void Start()
    {
        if (KongGameManager.kongLevelWon)
        {
            kongCollider.enabled = false;
        }
    }
}
