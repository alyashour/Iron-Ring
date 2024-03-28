using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class ForestTrigger : MonoBehaviour
{
    private string currentScene;

   
    private void LoadForest(string sceneName)
    {
        currentScene = sceneName;

        Camera camera = Camera.main;

        if (camera != null)
        {
            camera.cullingMask = 0;
        }
        Invoke(nameof(LoadForestScene), 1f);
    }

    private void LoadForestScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DontDestroyOnLoad(gameObject);
            LoadForest("KongScene");
        }
    }

}
