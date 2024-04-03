using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeBG : MonoBehaviour
{
    [SerializeField] Image background;

    private float t;
    private Color startColorBG;
    private Color startColorB;
    private float alpha = 0;

    private float fadeDuration = 2.5f;
    private float startTime;

    private void Start()
    {
        startColorBG = background.color;
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
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}