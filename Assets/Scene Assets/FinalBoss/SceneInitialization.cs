using Cinemachine;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Aiden

public class SceneInitialization : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject healthBarDisplay;

    [SerializeField] GameObject gameWinPrefab;

    [SerializeField] float doorYValue;

    public int sceneState = 0;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private float originalSize = 0.9f;
    private float zoomFactor = 2;
    private float currentCamSize;
    private float t = 0;

    private RingBossBehaviour bossb;

    private void Start()
    {
        Door.SetActive(false);
        bossb = GameObject.Find("RingBoss").GetComponent<RingBossBehaviour>();
    }

    private void Update()
    {
        // Walking up hallway
        if (player.transform.position.y > -5 && sceneState == 0)
        {
            sceneState = 1;
        }

        // In arena
        if (sceneState == 1)
        {
            if (player.transform.position.y > doorYValue)
            {
                Door.SetActive(true);
            }
            // If they are near the boss
            float dist = (player.transform.position - bossb.transform.position).magnitude;
            if (dist < 6.1f)
            {
                sceneState = 2;
                Instantiate(healthBarDisplay, GameObject.Find("Player").transform);
            }
            if (bossb.enemyHealth <= 0)
            {
                sceneState = 3;
            }
        }

        if (sceneState >= 1 && currentCamSize < (originalSize * zoomFactor))
        {
            currentCamSize = Mathf.Lerp(originalSize, originalSize * zoomFactor, t);
            t += 0.25f * Time.deltaTime;
            virtualCamera.m_Lens.OrthographicSize = currentCamSize;
        }

        if (!PlayerAttributes.Alive)
        {
            sceneState = -5;
        }

        if (sceneState == 10)
        {
            Instantiate(gameWinPrefab);
            if (GameObject.FindGameObjectsWithTag("HUD").Length > 0) {
                GameObject.FindGameObjectsWithTag("HUD")[0].SetActive(false);
            }
            sceneState = 11;
        }
    }
    
}