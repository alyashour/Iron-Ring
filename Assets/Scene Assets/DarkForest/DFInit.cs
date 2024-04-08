using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DFInit : MonoBehaviour
{
    [SerializeField] GameObject enterPac;
    private void Start()
    {
        if (PlayerAttributes.GlobalGameState == 4)
        {
            GameObject.Find("Player").transform.position = new Vector3(1f, 0, 0);
            enterPac.SetActive(false);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("LabrynthBoss");
    }
}
