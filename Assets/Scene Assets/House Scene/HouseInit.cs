using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInit : MonoBehaviour
{
    private void Start()
    {
        if (PlayerAttributes.GlobalGameState == 0)
        {
            GameObject.Find("Player").transform.position = new Vector3(1.107f, 0.114f, -1);
        } else
        {
            GameObject.Find("Player").transform.position = new Vector3(0.198f, -0.013f, -1);
        }
        PlayerAttributes.PlayerSpeed = 0.5f;
    }
}