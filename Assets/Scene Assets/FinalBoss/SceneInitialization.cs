using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialization : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject Door;

    [SerializeField] float doorYValue;

    public static int sceneState = 0;


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
        if (player.transform.position.y > doorYValue && sceneState == 0)
        {
            sceneState = 1;
            Door.SetActive(true);



        }


        // If they are near the boss
        if (sceneState == 1 && player.transform.position.y > 7)
        {
            sceneState = 2;
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