using Cinemachine;
using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class SceneInitialization : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject healthBarDisplay;

    [SerializeField] float doorYValue;

    public int sceneState = 0;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private float originalSize = 0.9f;
    private float zoomFactor = 2;
    private float currentCamSize;
    private float t = 0;

    private void Start()
    {
        Door.SetActive(false);
    }

    private void Update()
    {
        if (player.transform.position.y > -5 && sceneState == 0)
        {
            sceneState = 1;
        }

        if (player.transform.position.y > doorYValue && sceneState == 1)
        {
            Door.SetActive(true);
        }

        // If they are near the boss
        if (sceneState == 1 && player.transform.position.y > 6.5)
        {
            sceneState = 2;
            Instantiate(healthBarDisplay, GameObject.Find("Player").transform);
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
    }
}