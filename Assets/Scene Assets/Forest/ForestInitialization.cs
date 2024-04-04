using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestInitialization : MonoBehaviour
{
    private void Start()
    {
        if (PlayerAttributes.KongComplete) 
        {
            GameObject.Find("Player").transform.position = new Vector3(0.972f, -0.014f, -1);
            GameObject.Find("InitiateMagicianDialogue").SetActive(false);
            GameObject.Find("DonkeyKong").SetActive(false);
        } else
        {
            GameObject.Find("Player").transform.position = new Vector3(-1.177f, -0.014f, -1);
        }
    }
}
