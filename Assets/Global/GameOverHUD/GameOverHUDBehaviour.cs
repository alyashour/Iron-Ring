using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Behaviour script for the HUD to be displayed when the player dies
// Author: Aiden

public class GameOverHUDBehaviour : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image loadImage;
    [SerializeField] Image quitImage;
    [SerializeField] TextMeshProUGUI gameOverText;

    private float t;
    private Color startColorBG;
    private Color startColorB;
    private float alpha = 0;

    private float fadeDuration = 2.5f;
    private float startTime;

    private void Start()
    {
        startColorBG = background.color;
        startColorB = loadImage.color;
        startTime = Time.time;
    }

    private void Update()
    {
        // Gets the alpha
        t = (Time.time - startTime) / fadeDuration;
        alpha = Mathf.Lerp(0, 1, t);

        // Sets color of bg
        Color newColor = startColorBG;
        newColor.a = alpha;
        background.color = newColor;

        // Sets color of buttons
        Color newButtonColor = startColorB;
        newButtonColor.a = alpha;
        loadImage.color = newButtonColor;
        quitImage.color = newButtonColor;

        // Sets color of text
        Color newTextC = gameOverText.color; 
        newTextC.a = alpha;
        gameOverText.color = newTextC;
    }


    public void LoadLastSave()
    {
        InitializeGame.Load();
        SceneManager.LoadScene(PlayerAttributes.CurrentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}