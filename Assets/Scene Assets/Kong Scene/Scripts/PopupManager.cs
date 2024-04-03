using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    public GameObject tiePopup; // Assign this in the inspector
    public static PopupManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject foundTiePopup = GameObject.FindWithTag("PopUp");

        if (foundTiePopup != null)
        {
            tiePopup = foundTiePopup;
            Debug.Log("tie reassigned");
        } else
        {
            Debug.LogError("Failed to find tie");
            tiePopup = null;
        }
    }
    public void ShowTiePopup()
    {
        if (tiePopup != null)
            tiePopup.SetActive(true);
        else
            Debug.LogError("Tie popup not assigned.");
    }

    public void HideTiePopup()
    {
        if (tiePopup != null)
            tiePopup.SetActive(false);
        else
            Debug.LogError("Tie popup not assigned.");
    }
}
