using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KongGameManager : MonoBehaviour
{

    private int kongLives;
    private bool kongLevelWon = false;
    private string currentScene;
    public static KongGameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        KongNewGame();
    }

    private void KongNewGame()
    {
        kongLives = 3; kongLevelWon = false;
        LoadKongLevel("KongScene");
    }

    private void LoadKongLevel(string sceneName)
    {
        currentScene = sceneName;

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
        switch (currentScene)
        {
            case "KongScene":
                Debug.Log("Yo");
                LoadKongLevel("Forest");
                break;
            default:
                LoadKongLevel("KongScene");
                break;
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
