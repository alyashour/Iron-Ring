using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KongGameManager : MonoBehaviour
{

    private int kongLives;
    private bool kongLevelWon = false;
    private int currentScene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        KongNewGame();
    }

    private void KongNewGame()
    {
        kongLives = 3; kongLevelWon = false;
        LoadKongLevel(3);
    }

    private void LoadKongLevel(int index)
    {
        currentScene = index;

        Camera camera = Camera.main;

        if (camera != null )
        {
            camera.cullingMask = 0;
        }
        Invoke(nameof(LoadKongScene), 1f);
        
    }

    private void LoadKongScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void KongLevelComplete()
    {
        kongLevelWon = true;
        int nextScene = currentScene + 1;
        if (nextScene == 5)
        {
            LoadKongLevel(3);
        } else
        {
            LoadKongLevel(nextScene);
        }
    }
    public void KongLevelFailed()
    {
        kongLives--;
        if (kongLives == 0)
        {
            Debug.Log("Kong Game Lost! Out of Lives");
            KongNewGame();
        } else
        {
            LoadKongLevel(currentScene);
        }
    }


}
