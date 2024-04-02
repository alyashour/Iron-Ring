using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KongGameManager : MonoBehaviour
{

    private int kongLives;
    public static bool kongLevelWon = false;
    private string currentScene;
    public static KongGameManager instance;
    public static GameOverHUDBehaviour gameOverHUD;
    public Canvas tiePopup;
    public float popupDuration = 3f;
    public PopupManager popupManager;

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
        //DontDestroyOnLoad(gameObject);
        KongNewGame();
        if (tiePopup == null)
        {
            Debug.LogError("Tie popup not assigned in the Inspector.");
        }
        else { Debug.Log("Tie assigned");
                Debug.Log(tiePopup.gameObject.name);
        }
        Debug.Log("Hi");
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
                // LoadKongLevel("Forest");
                ShowTiePopup();
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

    public void ShowTiePopup()
    {
        Debug.Log("SHowing tie");
        popupManager.ShowTiePopup();
        StartCoroutine(HidePopupAndLoadScene(popupDuration, "Forest"));
    }
    IEnumerator HidePopupAndLoadScene(float duration, string sceneName)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Hiding Tie");
        popupManager.HideTiePopup();
        LoadKongLevel(sceneName);
    }


}
